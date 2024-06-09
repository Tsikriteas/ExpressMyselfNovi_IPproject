namespace ExpressMyselfNovi.Models
{
	public class Country
	{
		//TO FIX annotations
		public int Id { get; set; }
		public string Name { get; set; }
		public string TwoLetterCode { get; set; }
		public string ThreeLetterCode { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
