namespace ExpressMyselfNovi.Models
{
	public class IPinfo
	{
		public int Id { get; set; }
		public int CountryId { get; set; }
		public required string IP {  get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set;}
		public  Country Country { get; set; }
	}
}
