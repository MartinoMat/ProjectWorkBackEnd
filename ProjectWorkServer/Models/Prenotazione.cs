using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectWorkServer.Models
{
	public class Prenotazione
	{
		[Key]
		public int PrenotazioneId { get; set; }
		public required int RepartoId { get; set; }
		public required int EsameId { get; set; }
		public required DateOnly Data { get; set; }
		public required TimeOnly Orario { get; set; }
		public string? Riservato { get; set; }

	}
	public class InfoPren
	{
		[Key]
		public int PrenotazioneId { get; set; }
		public required int RepartoId { get; set; }
		public required string NomeReparto { get; set; }
		public required int EsameId { get; set; }
		public required string NomeEsame { get; set; }
		public required DateOnly Data { get; set; }
		public required TimeOnly Orario { get; set; }
		public string? Riservato { get; set; }

	}
	public class Reparto
	{
		[Key]
		public int RepartoId { get; set; }
		public required string Nome_Reparto { get; set; }
		public string? Desc_Reparto { get; set; }
		public List<Esame>? Esami { get; set; }
	}

	public class Esame
	{
		[Key]
		public int EsameId { get; set; }
		public required int RepartoId { get; set; }
		public required string Nome_Esame { get; set; }
		public string? Desc_Esame { get; set; }
		public List<Prenotazione>? Prenotazione { get; set; }
	}

	public class PrenotazAlt
	{
		public required int PrenotazioneId { get; set; }
        public required string userId { get; set; }
    }
}