using System.ComponentModel.DataAnnotations;

namespace ProjectWorkServer.Models
{
	/// <summary>
	/// Credenziali per la verifica dell'utente.
	/// Necessarie per recuperare un token JWT.
	/// </summary>
	public class UserLogin
	{
		/// <summary>
		/// Codice fiscale utente.
		/// </summary>
		[Required]
		public required string CodiceFiscale { get; set; }
		/// <summary>
		/// Password NON in chiaro utente.
		/// </summary>
		[Required]
		public required string Password { get; set; }
	}
}