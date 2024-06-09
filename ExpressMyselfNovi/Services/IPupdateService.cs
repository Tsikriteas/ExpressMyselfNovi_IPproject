using ExpressMyselfNovi.Data;
using ExpressMyselfNovi.Helpers;
using ExpressMyselfNovi.Interfaces;
using System.Threading;

namespace ExpressMyselfNovi.Services
{
	public class IPupdateService
	{
		private readonly Timer _timer;
		private readonly Ip2cService _ip2CService;
		private readonly IpappDb _context;
		private readonly CacheService _cacheService;
		private readonly ICountryDtoFactory _countryDtoFactory;

		public IPupdateService(Ip2cService ip2CService, IpappDb context, ICountryDtoFactory countryDtoFactory, CacheService cacheService) 
		{
			_context = context;
			_ip2CService = ip2CService;
			_cacheService = cacheService;
			_countryDtoFactory = countryDtoFactory;
			_timer = new Timer(Update, null, TimeSpan.Zero, TimeSpan.FromHours(1));

		}
		//TO see private
		public async void Update(Object state) 
		{
			try 
			{
				//get all ips
				var ips = _context.IPAddresses.Select(ip => ip.IP).ToList();
				var batches = ips.Select((ip, index) => new {ip, index })
					.GroupBy(x => x.index /100)
					.Select(g => g.Select(x => x.ip).ToList()).ToList();

				foreach ( var batch in batches) 
				{
					foreach(var ip in batch) 
					{
						var apiData = await _ip2CService.GetIPinfoAsync(ip);
						if (apiData == null) continue;

						var ipInfo = _countryDtoFactory.CreateFromApi(apiData);

						//find in database and if its diferent update
						var ipDB = _context.IPAddresses.FirstOrDefault(c => c.IP == ip);
						if (ipDB != null)
						{
							var country = ipDB.Country;
							if (country != null)
							{
								DateTime localDateTime = DateTime.Now;
								//if different update 
								if (ipInfo.Name != country.Name || ipInfo.TwoLetterCode != country.TwoLetterCode || ipInfo.ThreeLetterCode != country.ThreeLetterCode)
								{
									country.Name = ipInfo.Name;
									country.TwoLetterCode = ipInfo.TwoLetterCode;
									country.ThreeLetterCode = ipInfo.ThreeLetterCode;
									country.CreatedAt = localDateTime; 

									_context.SaveChangesAsync(); 
								}
							}
						}
						_cacheService.SetIPinfoToCache(ip, ipInfo);
					}
				}
			}
			catch (Exception ex) 
			{
				//cw ex if something wrong
			}
		}	
	}
}
