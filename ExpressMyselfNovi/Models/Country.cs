using System.ComponentModel.DataAnnotations;

namespace ExpressMyselfNovi.Models
{
	public class Country
	{
		//TO FIX annotations
		public int Id { get; set; }
		public required string Name { get; set; }
		public required string TwoLetterCode { get; set; }
		public required string ThreeLetterCode { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
