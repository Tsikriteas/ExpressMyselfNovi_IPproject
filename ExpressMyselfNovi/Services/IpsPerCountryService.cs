using ExpressMyselfNovi.Data;
using ExpressMyselfNovi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ExpressMyselfNovi.Services
{
	public class IpsPerCountryService
	{
		private readonly IpappDb _context;

		public IpsPerCountryService(IpappDb context) 
		{
			_context = context;
		}

		public async Task<List<IpsPerCountryDTO>> GetIpsPerCountry(string[] countryCodes = null) 
		{
			//to understand
			
				var codes = countryCodes != null && countryCodes.Any()
							? string.Join(",", countryCodes.Select(c => $"'{c}'"))
							: null;

			//sql guery as asked
				var query = @"
                SELECT c.Name AS CountryName, COUNT(i.IP) AS AddressesCount, MAX(i.UpdatedAt) AS LastAddressUpdated
                FROM Countries c
                JOIN IPAddresses i ON c.Id = i.CountryId
                {0}
                GROUP BY c.Name";

				var whereClause = codes != null ? $"WHERE c.TwoLetterCode IN ({codes})" : string.Empty;
				query = string.Format(query, whereClause);
			//create report table
				var ipReport = await _context.Database.SqlQueryRaw<IpsPerCountryDTO>(query).ToListAsync();

				return ipReport;
			
		}
	}
}
