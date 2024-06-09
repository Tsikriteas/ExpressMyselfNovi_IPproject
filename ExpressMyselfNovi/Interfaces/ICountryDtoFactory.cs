using ExpressMyselfNovi.Helpers;
using ExpressMyselfNovi.Models;

namespace ExpressMyselfNovi.Interfaces
{
	public interface ICountryDtoFactory
	{
		CountryDTO CreateFromDatabase(IPinfo dbInfo);
		CountryDTO CreateFromCache(CountryDTO cachedInfo);
		CountryDTO CreateFromApi(string[] apiData);
	}
}
