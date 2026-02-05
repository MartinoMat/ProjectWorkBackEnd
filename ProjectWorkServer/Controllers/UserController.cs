using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectWork.Server.Models;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ProjectWorkServer.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class UserController : Controller
	{
		private readonly AppDbContext _context;

		public UserController(AppDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Permette di registrare un nuovo utente nel database
		/// </summary>
		/// <param name="user">Riceve in input una classe utente (UserId è calcolato autonomamente)</param>
		/// <returns code="200">Restituisce 200OK se l'utente è stato registrato correttamente</returns>
		/// <returns code="400">Restituisce BadRequest in caso di errore</returns>
		[HttpPost("Add")]
		public async Task<IActionResult> AddUser([FromBody] User user)
		{
			try
			{
				user.UserId = Methods.ComputeSHA256(user.Codice_Fiscale + user.Nome + user.Cognome);
				user.PasswordHash = Methods.SaltedPassword(user.PasswordHash, user.UserId);

				_context.User.Add(user);
				await _context.SaveChangesAsync();

				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Inviando l'userId nel body, restituisce i dati dell'utente corrispondente se presente, altrimenti restituisce un messaggio di errore.
		/// </summary>
		/// <param name="userId">Richiede in input l'UserId dell'utente</param>
		/// <returns code="200">Restituisce 200OK se l'utente è stato trovato e risponde con un oggetto User contentente i dati dell'utente esclusa psw</returns>
		/// <returns code="404">Restituisce 404NotFound se l'utente non viene trovato</returns>
		/// <returns code="400">Restituisce 400BadRequest in caso di errore generico</returns>
		[HttpPost("UserInfo")]
		public async Task<IActionResult> GetUserData([FromBody] string userId)
		{
			try
			{
				var user = await _context.User.Where(x => x.UserId == userId).Select(u => new UserInfo
				{
					UserId = u.UserId,
					Codice_Fiscale = u.Codice_Fiscale,
					Nome=u.Nome,
					Cognome=u.Cognome,
					Genere = u.Genere,
					Compleanno = u.Compleanno,
					Com_Nascita = u.Com_Nascita,
					Com_Residenza = u.Com_Residenza,
					Ind_Residenza = u.Ind_Residenza,
					Email = u.Email
				}).FirstOrDefaultAsync();

				if (user == null) { return NotFound("Utente non trovato"); }					
				return Ok(user);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

#if DEBUG
		/// <summary>
		/// Get che stampa tutta l'elenco utenti, disponibile solo in ambiente di sviluppo 
		/// (utile per evitare l'utilizzo di un ulteriore tab postgre per prendere dati per effettuare testing)
		/// </summary>
		/// <returns></returns>
		[HttpGet("DEV_UserGet")]
		public async Task<IActionResult> GetUser()
		{
			var result = await _context.User.Select(x => new User
			{
				UserId = x.UserId,
				Codice_Fiscale = x.Codice_Fiscale,
				Nome = x.Nome,
				Cognome = x.Cognome,
				Genere = x.Genere,
				Compleanno = x.Compleanno,
				Com_Nascita = x.Com_Nascita,
				Com_Residenza = x.Com_Residenza,
				Ind_Residenza = x.Ind_Residenza,
				Email = x.Email,
				PasswordHash = x.PasswordHash,
			}).ToListAsync();

			return Ok(result);
		}
#endif

	}
}
