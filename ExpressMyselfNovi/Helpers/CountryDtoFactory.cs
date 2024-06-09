using ExpressMyselfNovi.Interfaces;
using ExpressMyselfNovi.Models;

namespace ExpressMyselfNovi.Helpers
{
	public class CountryDtoFactory :ICountryDtoFactory
	{
		public CountryDTO CreateFromDatabase(IPinfo dbinfo) 
		{
			return new CountryDTO 
			{
				Id = dbinfo.Country.Id.ToString(),
				Name = dbinfo.Country.Name,
				TwoLetterCode = dbinfo.Country.TwoLetterCode,
				ThreeLetterCode = dbinfo.Country.ThreeLetterCode,
			};
		}

		public CountryDTO CreateFromCache(CountryDTO cachedInfo) 
		{
			return new CountryDTO 
			{
				Id= cachedInfo.Id,
				Name = cachedInfo.Name,
				TwoLetterCode= cachedInfo.TwoLetterCode,
				ThreeLetterCode= cachedInfo.ThreeLetterCode,
			};
		}

		public CountryDTO CreateFromApi(string[] apiData) 
		{
			return new CountryDTO 
			{
				Id = apiData[0],
				TwoLetterCode = apiData[1],
				ThreeLetterCode= apiData[2],
				Name = apiData[3]
			};
		}
	}
}
