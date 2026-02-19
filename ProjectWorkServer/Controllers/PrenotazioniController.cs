using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectWork.Server.Models;
using ProjectWorkServer.Models;

namespace ProjectWorkServer.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PrenotazioniController : Controller
	{
		private readonly AppDbContext _context;

		public PrenotazioniController(AppDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Restituisce la lista gli orari disponibili per ogni esame (Riservato == null), raggruppati per data, esame e reparto. 
		/// {reparto:[{esame:[{data:[orari]}]}]}
		/// </summary>
		/// <returns code="200">Restituisce 200OK con la lista delle disponibilità</returns>
		/// <returns code="400">Restituisce 400BadRequest in caso di errore generico</returns>
		[HttpGet("Esami")]
		public async Task<IActionResult> GetReparti()
		{
			var result = await _context.Reparto
				.Select(r => new 
				{
					r.RepartoId,
					r.Nome_Reparto,
					r.Desc_Reparto,
					Esami = r.Esami.Select(e => new {
						e.EsameId,
						e.Nome_Esame,
						e.Desc_Esame,
						Prenotazione= e.Prenotazione.Where(q=>q.Riservato==null)
							.GroupBy(g => g.Data)
							.Select(g => new {
									Data = g.Key,
									Orari = g.Select(o => new {
											o.PrenotazioneId,
											o.Orario
									}).ToList()
							}).ToList()
					}).ToList()
			}).ToListAsync();

			return Ok(result);
		}

		/// <summary>
		/// Riserva uno slot orario per un esame, se disponibile.
		/// La prenotazione viene identificata da PrenotazioneId ma anche da RepartoId, EsameId, Data e Orario come controllo di ridondanza.
		/// Se lo slot è disponibile (Riservato == null), viene aggiornato con UserId in Riservato.
		/// </summary>
		/// <param name="request">Riceve in input un oggetto di tipo Prenotazione</param>
		/// <returns code="200">Restituisce 200OK se l'utente è assegnato correttamente aggiornata</returns>
		/// <returns code="400">Restituisce 400BadRequest in caso di errore generico</returns>
		[HttpPut("Prenota")]
		public async Task<IActionResult> PrenotaEsame([FromBody] Prenotazione request)
		{
			try
			{
				var prenota = await _context.Prenotazione.FirstOrDefaultAsync(x =>
					x.PrenotazioneId == request.PrenotazioneId &&
					x.RepartoId == request.RepartoId &&
					x.EsameId == request.EsameId &&
					x.Data == request.Data &&
					x.Orario == request.Orario &&
					x.Riservato== null
				);

				if (prenota == null)
				{
					return NotFound("Disponibilità non trovata");
				}

				prenota.Riservato = request.Riservato;

				await _context.SaveChangesAsync();

				return Ok("Appuntamento Confermato");
			}
			catch (Exception ex)
			{
				return BadRequest("errore: " + ex);
			}
		}

		/// <summary>
		/// libera uno slot orario per un esame.
		/// La prenotazione viene identificata da PrenotazioneId ma anche da RepartoId, EsameId, Data e Orario come controllo di ridondanza.
		/// Se lo slot è assegnato (Riservato == UserId), viene aggiornato con null in Riservato.
		/// </summary>
		/// <param name="request">Riceve in input un oggetto di tipo Prenotazione</param>
		/// <returns code="200">Restituisce 200OK se l'utente è rimosso correttamente aggiornata</returns>
		/// <returns code="400">Restituisce 400BadRequest in caso di errore generico</returns>
		[HttpPut("AnnullaPren")]
		public async Task<IActionResult> AnnullaEsame([FromBody] Prenotazione request)
		{
			try
			{
				var prenota = await _context.Prenotazione.FirstOrDefaultAsync(x =>
					x.PrenotazioneId == request.PrenotazioneId &&
					x.RepartoId == request.RepartoId &&
					x.EsameId == request.EsameId &&
					x.Data == request.Data &&
					x.Orario == request.Orario &&
					x.Riservato == request.Riservato
				);

				if (prenota == null)
				{
					return NotFound("Prenotazione non trovata");
				}

				prenota.Riservato = null;

				await _context.SaveChangesAsync();

				return Ok("Appuntamento Cancellato Correttametne");
			}
			catch (Exception ex)
			{
				return BadRequest("errore: " + ex);
			}
		}

		/// <summary>
		/// Richiede la lista di tutte le prenotazioni di un utente.
		/// </summary>
		/// <param name="userId">Richiede l'UserId per </param>
		/// <returns></returns>
		[HttpPost("PrenotazioniUser")]
		public async Task<IActionResult> GetPrenotazioni([FromBody] string userId)
		{
			try
			{
				var pren = await _context.Prenotazione.Where(x => x.Riservato == userId).Select(p => new Prenotazione
				{
					PrenotazioneId=p.PrenotazioneId,
					RepartoId=p.RepartoId,
					EsameId=p.EsameId,
					Data = p.Data,
					Orario = p.Orario,
					Riservato = p.Riservato
				}).ToListAsync();

				if (pren == null) { return NotFound("Utente non trovato"); }
				return Ok(pren);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
