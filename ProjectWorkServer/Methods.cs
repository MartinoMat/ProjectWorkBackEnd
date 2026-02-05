using ProjectWork.Server.Models;
using System.Security.Cryptography;
using System.Text;

namespace ProjectWorkServer
{
	public class Methods
	{
		/// <summary>
		/// Calcola l'hash SHA256 di una stringa.
		/// </summary>
		/// <param name="input">stringa in input di cui eseguire l'hash</param>
		/// <returns>Hash SHA256 della stringa data in input</returns>
		public static string ComputeSHA256(string input)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(input);
			byte[] hashBytes = SHA256.HashData(bytes);
			return Convert.ToHexString(hashBytes).ToLower();
		}

		/// <summary>
		/// Calcola l'hash della password combinata con parte dell'userid.
		/// </summary>
		/// <param name="password">Hash della password inviata dal client</param>
		/// <param name="userid">Userid calcolato dal server.</param>
		/// <returns>Hash della password col salt</returns>
		public static string SaltedPassword(string password, string userid)
		{
			return ComputeSHA256(password + userid.Substring(userid.Length / 4, userid.Length * 3 / 4));
		}

	}
}