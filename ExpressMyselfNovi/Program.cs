
using ExpressMyselfNovi.Data;
using ExpressMyselfNovi.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace ExpressMyselfNovi
{
	public class Program
	{
		public static void Main(string[] args)
		{

			var builder = WebApplication.CreateBuilder(args);
			//connection string from json
			var connectionString = builder.Configuration.GetConnectionString("UserContextConnection")
				?? throw new InvalidOperationException("Connection string 'UserContextConnection' not found.");

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

			//db configuration 
			builder.Services.AddDbContext<IpappDb>(options => options.UseSqlServer(connectionString));
			builder.Services.AddScoped<IpService>();
			builder.Services.AddScoped<IPupdateService>();
			builder.Services.AddScoped<IpsPerCountryService>();
			builder.Services.AddSingleton<CacheService>();
			builder.Services.AddHttpClient<Ip2cService>();
			builder.Services.AddMemoryCache();


			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			//seed
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				try
				{
					var context = services.GetRequiredService<IpappDb>();
					context.Database.Migrate();
					SeedData.Initialize(services);
				}
				catch (Exception ex)
				{
					//var logger = services.GetRequiredService<ILogger>();
					//logger.LogError(ex, "An error has occured seeding DB");
				}
			}

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
