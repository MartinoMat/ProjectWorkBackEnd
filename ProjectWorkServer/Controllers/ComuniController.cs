using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectWork.Server.Models;
using ProjectWorkServer.Models;

namespace ProjectWorkServer.Controllers
{
	public class ComuniController : Controller
	{
		private readonly AppDbContext _context;

		public ComuniController(AppDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Restituisce lista di dettagli dei comune in base al nome fornito.
		/// </summary>
		/// <param name="nome">Nome del comune da cercare</param>
		/// <returns>Restituisce lista di Codice Catastale, Nome e Provincia dei comune contententi quel nome</returns>
		[HttpGet("Codice")]
		public async Task<ActionResult<IEnumerable<CodiceComune>>> GetComune([FromQuery] string nome)
		{
			if (string.IsNullOrWhiteSpace(nome))
			{
				return BadRequest("Il parametro 'nome' è obbligatorio.");
			}

			var risultati = await _context.CodiceComune
				.Where(c => c.Comune.Contains(nome.ToUpper())).ToListAsync();

			if (risultati is null)
			{
				return NotFound($"Nessun comune trovato con il nome: {nome}");
			}

			return Ok(risultati);
		}		
	}
}
