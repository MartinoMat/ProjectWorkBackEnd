using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProjectWorkServer.Models
{
	[Keyless]
	public class CodiceComune
	{
		/// <summary>
		/// Codice Catastale del comune.
		/// </summary>
		public string? CodiceCatastale { get; set; }
		/// <summary>
		/// Nome del comune.
		/// </summary>
		public string? Comune { get; set; }
		/// <summary>
		/// Provincia in cui risiede il comune.
		/// </summary>
		public string? Provincia { get; set; }
	}
}
