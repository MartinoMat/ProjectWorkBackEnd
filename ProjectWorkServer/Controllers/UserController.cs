using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectWork.Server.Models;
using Sprache;
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
		/// <param name="user">Richiede in input una classe utente (UserId è calcolato autonomamente)</param>
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
		public async Task<IActionResult> GetUserInfo([FromBody] string userId)
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

		/// <summary>
		/// Aggiorna i dati di un utente, richiede in input una classe User con l'userId e Password (non aggiornabili) e i dati da aggiornare.
		/// </summary>
		/// <param name="request">Richiede in input un formato UserInfo</param>
		/// <returns code="200">Restituisce 200OK se l'utente è stato trovato aggiorna i dati dell'utente esclusa password</returns>
		/// <returns code="400">Restituisce 400BadRequest in caso di errore generico</returns>
		[HttpPut("UserUpdate")]
		public async Task<IActionResult> UpdateUser([FromBody] User request)
		{
			try
			{
				var user = await _context.User.FirstOrDefaultAsync(x =>
				x.UserId == request.UserId &&
				x.PasswordHash== Methods.SaltedPassword(request.PasswordHash, request.UserId));

				if (user == null)
				{
					return NotFound("Utente non trovato");
				}

				user.Genere = request.Genere;
				user.Compleanno = request.Compleanno;
				user.Com_Nascita = request.Com_Nascita;
				user.Com_Residenza = request.Com_Residenza;
				user.Ind_Residenza = request.Ind_Residenza;
				user.Email = request.Email;

				await _context.SaveChangesAsync();

				return Ok("Utente aggiornato");
			}
			catch (Exception ex)
			{
				return BadRequest("errore: " + ex);
			}
		}

		/// <summary>
		/// Permette di aggiornare la password di un utente, richiede in input l'userId, la password attuale e la nuova password.
		/// Se l'userId e la password attuale corrispondono a un utente presente nel database.
		/// Aggiorna la password con quella nuova, altrimenti restituisce un messaggio di errore.
		/// </summary>
		/// <param name="request"></param>
		/// <returns code="200">Restituisce 200OK se l'utente è stato trovato e la password correttamente aggiornata</returns>
		/// <returns code="400">Restituisce 400BadRequest in caso di errore generico</returns>
		[HttpPut("UserPswUpdate")]
		public async Task<IActionResult> UpdatePsw([FromBody] UserPsw request)
		{
			try
			{
				var user = await _context.User.FirstOrDefaultAsync(x =>
					x.UserId == request.UserId &&
					x.PasswordHash == Methods.SaltedPassword(request.PasswordHash, request.UserId)
				);

				if (user == null)
				{
					return NotFound("Utente non trovato");
				}

				user.PasswordHash = Methods.SaltedPassword(request.PasswordNew, request.UserId);

				await _context.SaveChangesAsync();

				return Ok("Password aggiornata");
			}
			catch (Exception ex)
			{
				return BadRequest("errore: " + ex);
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
