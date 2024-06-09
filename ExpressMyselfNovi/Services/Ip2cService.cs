using ExpressMyselfNovi.Helpers;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using ExpressMyselfNovi.Models;
using System.Diagnostics.Metrics;

namespace ExpressMyselfNovi.Services
{
	public class Ip2cService
	{
		private readonly HttpClient _httpClient;

		public Ip2cService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<CountryDTO> GetIPinfoAsync(string ip) 
		{
			string url = $"https://ip2c.org/{ip}";

			HttpResponseMessage response = await _httpClient.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				//141.237.193.10
				string jsonResponse = await response.Content.ReadAsStringAsync();

				string[] parts = jsonResponse.Split(';');
				if (parts.Length >= 4)
				{
					string id = parts[0];
					string twoLetterCode = parts[1];
					string threeLetterCode = parts[2];
					string name = parts[3];

					CountryDTO country = new CountryDTO
					{
						Id = id,
						TwoLetterCode = twoLetterCode,
						ThreeLetterCode = threeLetterCode,
						Name = name
					};
					return country;

				}

				return null;
			}
			else 
			{
				return null;
			}
		}
	}
}
