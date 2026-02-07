using System.ComponentModel.DataAnnotations;

namespace ProjectWork.Server.Models
{
	/// <summary>
	/// Classe Utente.
	/// </summary>
	public class User
	{
		/// <summary>
		/// Id univoco utente.
		/// </summary>		
		public required string UserId { get; set; }

		/// <summary>
		/// Codice fiscale utente.
		/// </summary>
		[Required]
		public required string Codice_Fiscale { get; set; }

		/// <summary>
		/// Nome utente.
		/// </summary>
		[Required]
		public required string Nome { get; set; }

		/// <summary>
		/// Cognome utente.
		/// </summary>
		[Required]
		public required string Cognome { get; set; }

		/// <summary>
		/// Genere (M/F) utente. La stringa deve essere lunga 1 carattere.
		/// </summary>
		[Required]
		public required char Genere { get; set; }

		/// <summary>
		/// Data di nascita utente.
		/// </summary>
		[Required]
		public required DateOnly Compleanno { get; set; }

		/// <summary>
		/// Comune Nascita utente.
		/// </summary>
		[Required]
		public required string Com_Nascita { get; set; }

		/// <summary>
		/// Comune Residenza utente.
		/// </summary>
		[Required]
		public required string Com_Residenza { get; set; }

		/// <summary>
		/// Indirizzo Residenza utente.
		/// </summary>
		[Required]
		public required string Ind_Residenza { get; set; }

		/// <summary>
		/// Email utente.
		/// </summary>
		[Required]
		public required string Email { get; set; }

		/// <summary>
		/// Hash della password utente.
		/// </summary>
		[Required]
		public required string PasswordHash { get; set; }
	}


	public class UserInfo
	{
		/// <summary>
		/// Id univoco utente.
		/// </summary>		
		[Key]
		public required string UserId { get; set; }

		/// <summary>
		/// Codice fiscale utente.
		/// </summary>
		[Required] 
		public required string Codice_Fiscale { get; set; }

		/// <summary>
		/// Nome utente.
		/// </summary>
		[Required] 
		public required string Nome { get; set; }

		/// <summary>
		/// Cognome utente.
		/// </summary>
		[Required] 
		public required string Cognome { get; set; }

		/// <summary>
		/// Genere (M/F) utente. La stringa deve essere lunga 1 carattere.
		/// </summary>
		[Required] 
		public required char Genere { get; set; }

		/// <summary>
		/// Data di nascita utente.
		/// </summary>
		[Required] 
		public required DateOnly Compleanno { get; set; }

		/// <summary>
		/// Comune Nascita utente.
		/// </summary>
		[Required]
		public required string Com_Nascita { get; set; }

		/// <summary>
		/// Comune Residenza utente.
		/// </summary>
		[Required] 
		public required string Com_Residenza { get; set; }

		/// <summary>
		/// Indirizzo Residenza utente.
		/// </summary>
		[Required] 
		public required string Ind_Residenza { get; set; }

		/// <summary>
		/// Email utente.
		/// </summary>
		 [Required]
		public required string Email { get; set; }
	}

	public class UserPsw
	{
		/// <summary>
		/// Id univoco utente.
		/// </summary>		
		[Key]
		public required string UserId { get; set; }
		/// <summary>
		/// Hash della password utente.
		/// </summary>
		[Required]
		public required string PasswordHash { get; set; }
		/// <summary>
		/// Hash della password utente.
		/// </summary>
		[Required]
		public required string PasswordNew { get; set; }
	}
}
