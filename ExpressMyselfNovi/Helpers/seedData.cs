using ExpressMyselfNovi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net;

namespace ExpressMyselfNovi.Data
{
	public static class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new IpappDb(
				serviceProvider.GetRequiredService<DbContextOptions<IpappDb>>()))
			{
				// Look for any data already in the database.
				if (context.Countries.Any() || context.IPAddresses.Any())
				{
					return;   // Database has been seeded
				}

				//conflict me foreign keys fixed

				var countries = new[]
				{
					new Country { Name = "Greece", TwoLetterCode = "GR", ThreeLetterCode = "GRC", CreatedAt = DateTime.Parse("2022-10-12T06:46:10.5000000") },
					new Country { Name = "Germany", TwoLetterCode = "DE", ThreeLetterCode = "DEU", CreatedAt = DateTime.Parse("2022-10-12T06:46:10.5000000") },
					new Country { Name = "Cyprus", TwoLetterCode = "CY", ThreeLetterCode = "CYP", CreatedAt = DateTime.Parse("2022-10-12T06:46:10.5000000") },
					new Country { Name = "United States", TwoLetterCode = "US", ThreeLetterCode = "USA", CreatedAt = DateTime.Parse("2022-10-12T06:46:10.5000000") },
					new Country { Name = "Spain", TwoLetterCode = "ES", ThreeLetterCode = "ESP", CreatedAt = DateTime.Parse("2022-10-12T06:46:10.5000000") },
					new Country { Name = "France", TwoLetterCode = "FR", ThreeLetterCode = "FRA", CreatedAt = DateTime.Parse("2022-10-12T06:46:10.5000000") },
					new Country { Name = "Italy", TwoLetterCode = "IT", ThreeLetterCode = "ITA", CreatedAt = DateTime.Parse("2022-10-12T06:46:10.5000000") },
					new Country { Name = "Japan", TwoLetterCode = "JP", ThreeLetterCode = "JPN", CreatedAt = DateTime.Parse("2022-10-12T06:46:10.5000000") },
					new Country { Name = "China", TwoLetterCode = "CN", ThreeLetterCode = "CHN", CreatedAt = DateTime.Parse("2022-10-12T06:46:10.5000000") }
				};

				context.Countries.AddRange(countries);
				context.SaveChanges();

				var countryLookup = context.Countries.ToDictionary(c => c.Name, c => c.Id);

				context.IPAddresses.AddRange(
					new IPinfo { CountryId = countryLookup["Greece"], IP = "44.255.255.254", CreatedAt = DateTime.Parse("2022-10-12T07:04:06.8566667"), UpdatedAt = DateTime.Parse("2022-10-12T07:04:06.8566667") },
					new IPinfo { CountryId = countryLookup["Germany"], IP = "45.255.255.254", CreatedAt = DateTime.Parse("2022-10-12T07:04:06.8566667"), UpdatedAt = DateTime.Parse("2022-10-12T07:04:06.8566667") },
					new IPinfo { CountryId = countryLookup["Cyprus"], IP = "46.255.255.254", CreatedAt = DateTime.Parse("2022-10-12T07:04:06.8566667"), UpdatedAt = DateTime.Parse("2022-10-12T07:04:06.8566667") },
					new IPinfo { CountryId = countryLookup["United States"], IP = "47.255.255.254", CreatedAt = DateTime.Parse("2022-10-12T07:04:06.8566667"), UpdatedAt = DateTime.Parse("2022-10-12T07:04:06.8566667") },
					new IPinfo { CountryId = countryLookup["France"], IP = "49.255.255.254", CreatedAt = DateTime.Parse("2022-10-12T07:04:06.8566667"), UpdatedAt = DateTime.Parse("2022-10-12T07:04:06.8566667") },
					new IPinfo { CountryId = countryLookup["Italy"], IP = "41.255.255.254", CreatedAt = DateTime.Parse("2022-10-12T07:04:06.8566667"), UpdatedAt = DateTime.Parse("2022-10-12T07:04:06.8566667") },
					new IPinfo { CountryId = countryLookup["Japan"], IP = "42.255.255.254", CreatedAt = DateTime.Parse("2022-10-12T07:04:06.8566667"), UpdatedAt = DateTime.Parse("2022-10-12T07:04:06.8566667") },
					new IPinfo { CountryId = countryLookup["China"], IP = "43.255.255.254", CreatedAt = DateTime.Parse("2022-10-12T07:04:06.8566667"), UpdatedAt = DateTime.Parse("2022-10-12T07:04:06.8566667") }
				);

				context.SaveChanges();
			}
		}
	}
}
