using Microsoft.Extensions.Caching.Memory;
using ExpressMyselfNovi.Helpers;
using ExpressMyselfNovi.Models;

namespace ExpressMyselfNovi.Services
{
	public class CacheService
	{
		private readonly IMemoryCache _memoryCache;
		private readonly TimeSpan _cacheDuration = TimeSpan.FromSeconds(1);

		public CacheService(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		public CountryDTO GetIPinfoFromCache(string ip) 
		{
			_memoryCache.TryGetValue(ip ,out CountryDTO ipAddress);
			return ipAddress;
		}

		public void SetIPinfoToCache(string ip, CountryDTO iPAddress) 
		{
			_memoryCache.Set(ip ,iPAddress, _cacheDuration);
		}
	}
}
