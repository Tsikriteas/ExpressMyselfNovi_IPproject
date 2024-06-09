using ExpressMyselfNovi.Helpers;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using ExpressMyselfNovi.Models;
using System.Diagnostics.Metrics;
using ExpressMyselfNovi.Interfaces;

namespace ExpressMyselfNovi.Services
{
	public class Ip2cService : Iip2cService
	{
		private readonly HttpClient _httpClient;

		public Ip2cService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		//use web service ip2c for data
		public async Task<string[]> GetIPinfoAsync(string ip) 
		{
			//request
			string url = $"https://ip2c.org/{ip}";
			//response
			HttpResponseMessage response = await _httpClient.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				string jsonResponse = await response.Content.ReadAsStringAsync();
				//response not in json
				string[] parts = jsonResponse.Split(';');
				if (parts.Length >= 4)
				{
					
					return parts;

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
