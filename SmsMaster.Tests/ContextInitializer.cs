using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SmsMaster.Data;
using System;
using System.Collections.Generic;
using System.Text;
using SmsMaster.Model;

namespace SmsMaster.Tests
{
    public class ContextInitializer
    {
        public List<Country> GenerateCountries()
        {
            return new List<Country>()
            {
                new Country()
                {
                    Name="Germany",
                    MobileCountryCode = "262",
                    CountryCode = "49",
                    PricePerSms = 0.055m
                },
                new Country()
                {
                    Name="Austria",
                    MobileCountryCode = "232",
                    CountryCode = "43",
                    PricePerSms = 0.053m
                },
                new Country()
                {
                    Name="Poland",
                    MobileCountryCode = "260",
                    CountryCode = "48",
                    PricePerSms = 0.032m
                }
            };
        }

        public SmsMasterContext SmsMasterContext { get; set; }

        public void Initialize()
        {
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
            var builder = new DbContextOptionsBuilder<SmsMasterContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseInternalServiceProvider(serviceProvider);
            SmsMasterContext = new SmsMasterContext(builder.Options);
            SmsMasterContext.Country.AddRange(GenerateCountries());
            SmsMasterContext.SaveChanges();
        }
    }
}
