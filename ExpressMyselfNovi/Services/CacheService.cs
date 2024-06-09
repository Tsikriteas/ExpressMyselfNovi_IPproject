using Microsoft.Extensions.Caching.Memory;
using ExpressMyselfNovi.Helpers;
using ExpressMyselfNovi.Models;
using ExpressMyselfNovi.Interfaces;

namespace ExpressMyselfNovi.Services
{
	public class CacheService : ICacheService
	{
		private readonly IMemoryCache _memoryCache;
		private readonly TimeSpan _cacheDuration = TimeSpan.FromSeconds(1);

		public CacheService(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}
		//get from cache
		public CountryDTO GetIPinfoFromCache(string ip) 
		{
			_memoryCache.TryGetValue(ip ,out CountryDTO ipAddress);
			return ipAddress;
		}

		//save to memory cache
		public void SetIPinfoToCache(string ip, CountryDTO iPAddress) 
		{
			_memoryCache.Set(ip ,iPAddress, _cacheDuration);
		}
	}
}
