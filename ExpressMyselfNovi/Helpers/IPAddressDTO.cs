﻿using ExpressMyselfNovi.Models;

namespace ExpressMyselfNovi.Helpers
{
	public class IPAddressDTO
	{
		public int Id { get; set; }
		public int CountryId { get; set; }
		public string IP { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public Country Country { get; set; }
	}
}
