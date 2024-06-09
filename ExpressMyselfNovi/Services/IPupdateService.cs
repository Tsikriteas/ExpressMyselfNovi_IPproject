using ExpressMyselfNovi.Data;
using System.Threading;

namespace ExpressMyselfNovi.Services
{
	public class IPupdateService
	{
		private readonly Timer _timer;
		private readonly Ip2cService _ip2CService;
		private readonly IpappDb _context;
		private readonly CacheService _cacheService;

		public IPupdateService(Ip2cService ip2CService, IpappDb context, CacheService cacheService) 
		{
			_context = context;
			_ip2CService = ip2CService;
			_cacheService = cacheService;
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
						var ipInfo = await _ip2CService.GetIPinfoAsync(ip);
						//find in database and if its diferent update
						var ipDB = _context.IPAddresses.FirstOrDefault(c => c.IP == ip);
						if (ipDB != null)
						{
							var country = _context.Countries.FirstOrDefault(c => c.Id == ipDB.CountryId);
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

									_context.SaveChanges(); 
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
