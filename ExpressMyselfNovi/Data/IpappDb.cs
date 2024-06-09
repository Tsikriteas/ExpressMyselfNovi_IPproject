using ExpressMyselfNovi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExpressMyselfNovi.Data
{
	public class IpappDb : DbContext
	{

		public DbSet<Country> Countries { get; set; }
		public DbSet<IPinfo> IPAddresses { get; set; }

		//na dw giati to thelw ayto
		public IpappDb(DbContextOptions<IpappDb> options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Country>().HasKey(c => c.Id);
			modelBuilder.Entity<IPinfo>().HasKey(ip => ip.Id);
			modelBuilder.Entity<IPinfo>().HasIndex(ip => ip.IP).IsUnique();
			modelBuilder.Entity<IPinfo>()
				.HasOne(ip => ip.Country)
				.WithMany()
				.HasForeignKey(ip => ip.CountryId);
		}
	}
}
