using ExpressMyselfNovi.Helpers;
using Microsoft.Extensions.Caching.Memory;

namespace ExpressMyselfNovi.Interfaces
{
	public interface ICacheService
	{
		CountryDTO GetIPinfoFromCache(string ip);
		void SetIPinfoToCache(string ip, CountryDTO iPAddress);
	}
}
