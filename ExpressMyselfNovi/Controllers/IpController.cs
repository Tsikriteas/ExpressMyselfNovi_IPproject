using ExpressMyselfNovi.Helpers;
using ExpressMyselfNovi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ExpressMyselfNovi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class IPinfoController : ControllerBase
	{
		private readonly IpService _ipService;
		private readonly IPupdateService _ipUpdateService;
		private readonly IpsPerCountryService _ipsPerCountryService;
		private readonly ILogger<IPinfoController> _logger;

		public IPinfoController(IpService ipService, IPupdateService ipUpdateService, IpsPerCountryService ipsPerCountryService, ILogger<IPinfoController> logger) 
		{
			_ipService = ipService;
			_ipUpdateService = ipUpdateService;
			_ipsPerCountryService = ipsPerCountryService;
			_logger = logger;
		}

		[HttpGet("{ip}")]
		public async Task<IActionResult> GetIPinfo(string ip)
		{
			CountryDTO ipInfo =await _ipService.GetIPinfo(ip);
			if (ipInfo == null)
			{
				return NotFound();
			}
			return Ok(ipInfo);
		}

		[HttpPost("updateIPs")]
		public IActionResult UpdateIPs() 
		{
			//to check
			_ipUpdateService.Update(null);
			return Ok("Update started");
		}

		[HttpGet("IpsPerCountry")]
		public async Task<IActionResult> GetIpsPerCountry() 
		{
			List<IpsPerCountryDTO> ipsPerCountry =await _ipsPerCountryService.GetIpsPerCountry();
			if (ipsPerCountry == null) 
			{
				return NotFound();
			}
			return Ok(ipsPerCountry);
		}

	}

}
