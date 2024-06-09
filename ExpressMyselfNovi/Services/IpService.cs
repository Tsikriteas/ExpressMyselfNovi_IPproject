using ExpressMyselfNovi.Data;
using ExpressMyselfNovi.Helpers;
using ExpressMyselfNovi.Interfaces;
using ExpressMyselfNovi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace ExpressMyselfNovi.Services
{
	public class IpService
	{
		private readonly IpappDb _context;
		private readonly CacheService _cacheService;
		private readonly Ip2cService _ip2cService;
		private readonly ICountryDtoFactory _countryDtoFactory;

		public IpService(IpappDb context, CacheService cacheService, Ip2cService ip2CService, ICountryDtoFactory countryDtoFactory) 
		{
			_context = context;
			_cacheService = cacheService;
			_ip2cService = ip2CService;
			_countryDtoFactory = countryDtoFactory;
		}
		public async Task<CountryDTO> GetIPinfo(string ip)
		{
			//Cache memory search
			CountryDTO ipInfo = _cacheService.GetIPinfoFromCache(ip);
			if (ipInfo != null)
			{
				return _countryDtoFactory.CreateFromCache(ipInfo); ;
			}

			//database search
			//TO FIX thelei await kai async
			var DBipInfo =await _context.IPAddresses
				.Include(x => x.Country)
				.FirstOrDefaultAsync(x => x.IP ==  ip);
			if (DBipInfo != null)
			{
				var ipInfos = _countryDtoFactory.CreateFromDatabase(DBipInfo);
				_cacheService.SetIPinfoToCache(ip, ipInfo);
				return ipInfos;
			}

			//IP2C call
			var apiData = await _ip2cService.GetIPinfoAsync(ip);
			if (apiData != null) 
			{
				ipInfo = _countryDtoFactory.CreateFromApi(apiData);

				DateTime localDateTime = DateTime.Now;
			
				var newCountry = new Country
				{
					Name = ipInfo.Name,
					TwoLetterCode = ipInfo.TwoLetterCode,
					ThreeLetterCode = ipInfo.ThreeLetterCode,
					CreatedAt = localDateTime
				};
				_context.Countries.Add(newCountry);

				await _context.SaveChangesAsync();
				var newIPAddress = new IPinfo
				{
					CountryId = newCountry.Id,
					IP = ip,
					CreatedAt = localDateTime,
					UpdatedAt = localDateTime
				};
				_context.IPAddresses.Add(newIPAddress);
				
				await _context.SaveChangesAsync();

				//add to cache
				_cacheService.SetIPinfoToCache(ip, ipInfo);
			}
			return ipInfo;
		}	 
	}
}
