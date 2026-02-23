using System.ComponentModel.DataAnnotations;

namespace ProjectWorkServer.Models
{
	public class Referto
	{
		[Key]
		public int PrenotazioneId { get; set; }
		public required string UserId { get; set; }
		public required string Nome_Esame { get; set; }
		public required DateOnly Data_Esame { get; set; }
		public required string Cognome { get; set; }
	}
}
