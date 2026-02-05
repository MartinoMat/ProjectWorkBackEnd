using DotNetEnv;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectWork.Server.Models;
using ProjectWorkServer.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProjectWorkServer.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthController : Controller
	{
		private readonly IConfiguration _configuration;

		private readonly AppDbContext _context;

		public AuthController(IConfiguration configuration, AppDbContext context)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

	   /// <summary>
	   /// Verifica le credenziali di accesso di un utente.
	   /// </summary>
	   /// <param name="request">Si aspetta dal body CodiceFiscale e Password</param>
	   /// <response code="200">Credenziali corrette. 
	   /// Restituisce un token JWT di autorizzazione se l'utente esiste con durata 1 giorno e submitter l'ID Utente</response>
	   /// <response code="401">Credenziali errate.</response>
	   /// <response code="400">Errore di tipo BadRequest.</response>
	   [HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] UserLogin request)
		{
			try
			{
				var result = await _context.User.Where(u => u.Codice_Fiscale == request.CodiceFiscale.ToUpper()).FirstOrDefaultAsync();

				if (result is not null)
				{
					var psw = Methods.SaltedPassword(request.Password, result.UserId);

					if (result.PasswordHash == psw)
					{ 
						Env.Load();

						List<Claim> ClaimList = new List<Claim>()
					{
						new Claim(JwtRegisteredClaimNames.Sub, result.UserId),
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
					};

						var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]
							.Replace("${JWT_KEY}", Environment.GetEnvironmentVariable("JWT_KEY"))));

						var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
						var tkn = new JwtSecurityToken(
							_configuration["Jwt:Issuer"],
							_configuration["Jwt:Audience"],
							ClaimList,
							expires: DateTime.Now.AddDays(1),
							signingCredentials: creds
						);

						return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(tkn) });
					}
					else
					{
						return Unauthorized("Credenziali non valide");
					}
				}
				else { return Unauthorized("Credenziali non valide"); /*restituisce comunque 401 per non divulgare la presenza o meno in DB*/}
			}
			catch (Exception ex)
			{
				return BadRequest("Errore: " + ex.Message);
			}
		}
	}
}