using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using ProjectWork.Server.Models;
using System.IO.Compression;
using ProjectWorkServer.Models;
using Sprache;

namespace ProjectWorkServer.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class RefertiController : Controller
	{
		private readonly AppDbContext _context;

		public RefertiController(AppDbContext context)
		{
			_context = context;
		}
		private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "Docs\\");


		/// <summary>
		/// Cerca tutte le prenotazioni associate all'userId fornito che hanno un referto disponibile (Referto == true)
		/// Restituisce una lista di oggetti InfoPren contenenti i dettagli delle prenotazioni, ordinati per data e orario.
		/// Se non viene trovato alcun utente o se si verifica un errore durante l'esecuzione della query, restituisce un messaggio di errore appropriato.
		/// </summary>
		/// <param name="userId">Riceve una richiesta contenente l'userId</param>
		/// <response code="200">Restituisce una lista di oggetti InfoPren con i dettagli delle prenotazioni</response>
		/// <response code="400">Restituisce un messaggio di errore se si verifica un errore durante l'esecuzione della query</response>
		/// <response code="404">Restituisce un messaggio di errore se non viene trovato alcun utente </response>
		[HttpPost("RefertiUser")]
		public async Task<IActionResult> GetPrenotazioni([FromBody] string userId)
		{
			try
			{
				var pren = await (from p in _context.Prenotazione
								  join r in _context.Reparto on p.RepartoId equals r.RepartoId
								  join e in _context.Esame on p.EsameId equals e.EsameId
								  where p.UserId == userId && p.Referto==true
								  select new InfoPren
								  {
									  PrenotazioneId = p.PrenotazioneId,
									  RepartoId = p.RepartoId,
									  NomeReparto = r.Nome_Reparto,
									  EsameId = p.EsameId,
									  NomeEsame = e.Nome_Esame,
									  Data = p.Data,
									  Orario = p.Orario,
									  UserId = p.UserId
								  }).OrderBy(d => d.Data).ThenBy(o => o.Orario).ToListAsync();

				if (pren == null) { return NotFound("Utente non trovato"); }
				return Ok(pren);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Verifica se esiste un referto associato alla prenotazione fornita e corrisponde all'utente fornito.
		/// Se presente, crea un file ZIP contenente i documenti del referto e lo restituisce come download.
		/// Se non ci sono referti disponibili o se la cartella specificata non esiste, restituisce un messaggio di errore appropriato
		/// </summary>
		/// <param name="req">Riceve una richiesta contenente l'userId e prenotazioneId</param>
		/// <response code="200">Restituisce un file ZIP con i documenti del referto</response>
		/// <response code="400">Restituisce un messaggio di errore se i dati della richiesta sono incompleti o se la cartella è vuota</response>
		/// <response code="404">Restituisce un messaggio di errore se non ci sono referti disponibili o se la cartella specificata non esiste</response>
		/// <response code="500">Restituisce un messaggio di errore se si verifica un errore durante la creazione dello ZIP</response>
		[HttpPost("Download")]
		public async Task<IActionResult> GetFileRef([FromBody] PrenotazAlt req)
		{
			try { 
				if (req == null || string.IsNullOrEmpty(req.userId))
				{
					return BadRequest("Dati della richiesta incompleti.");
				}

				var referto = await (from p in _context.Prenotazione
								  join u in _context.User on p.UserId equals u.UserId
								  join e in _context.Esame on p.EsameId equals e.EsameId
								  where p.UserId == req.userId &&
										p.PrenotazioneId == req.PrenotazioneId &&
										p.Referto == true &&
										p.Data <= DateOnly.FromDateTime(DateTime.Now)
								  select new Referto
								  {
									  PrenotazioneId = p.PrenotazioneId,
									  UserId = p.UserId,
									  Nome_Esame = e.Nome_Esame,
									  Data_Esame = p.Data,
									  Cognome = u.Cognome + u.Nome.Substring(0, 1)
								  }).FirstOrDefaultAsync();

				if (referto == null)
				{
					return NotFound("Nessun referto disponibile.");
				}

				var filePath = Path.Combine(_storagePath, req.userId, req.PrenotazioneId.ToString());

				if (!Directory.Exists(filePath))
				{
					return NotFound("La cartella specificata non esiste sul server.");
				}


				var memoryStream = new MemoryStream();

				using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
				{
					var files = Directory.GetFiles(filePath);

					if (files.Length == 0) return BadRequest("La cartella è vuota.");

					foreach (var file in files)
					{
						var fileInfo = new FileInfo(file);
						archive.CreateEntryFromFile(fileInfo.FullName, fileInfo.Name);
					}
				}
				memoryStream.Position = 0;
				return File(memoryStream, "application/zip", referto.Nome_Esame.Replace(" ", "") + "_"+ referto.Cognome+ "_" + referto.Data_Esame.ToString("yyyyMMdd") + ".zip");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Errore durante la creazione dello ZIP: {ex.Message}");
			}
		}


#if DEBUG
        /// <summary>
        /// Permette di caricare file di referti per scopi di test.
		/// Riceve un oggetto PrenotazAlt contenente userId e PrenotazioneId, insieme a una lista di file.
        /// </summary>
        /// <param name="data">Oggetto contenente userId e prenotazioneId</param>
        /// <param name="files">Lista di files da caricare</param>
        /// <response code="200">Restituisce un messaggio di avvenuto caricamento</response>
		/// <response code="400">Restituisce un messaggio di errore se i dati della richiesta sono incompleti o il tipo di file non è accettato</response>
        [HttpPost("Upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFiles([FromForm] PrenotazAlt data, List<IFormFile> files)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".docx" };

            if (files == null || files.Count == 0)
                return BadRequest("Nessun file selezionato.");

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
                {
                    return BadRequest($"Tipo di file non permesso: {file.FileName}. Estensioni consentite: {string.Join(", ", allowedExtensions)}");
                }
            }

            var targetPath = Path.Combine(_storagePath, data.userId, data.PrenotazioneId.ToString());

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(targetPath, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new { message = "Files caricati correttamente", path = targetPath });
        }
#endif
	}
}
