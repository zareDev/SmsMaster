using Microsoft.EntityFrameworkCore;
using SmsMaster.Model;
using System;
using SmsMaster.Data.Configuration;
namespace SmsMaster.Data
{

    public class SmsMasterContext : DbContext
    {
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Sms> Sms { get; set; }
        public SmsMasterContext(DbContextOptions<SmsMasterContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.CountryMapping();
            modelBuilder.SmsMapping();

            SeedData(modelBuilder);
        }

        protected void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country() { Name = "Germany", CountryCode = "49", MobileCountryCode = "262", PricePerSms = 0.055m },
                new Country() { Name = "Austria", CountryCode = "43", MobileCountryCode = "232", PricePerSms = 0.053m },
                new Country() { Name = "Poland", CountryCode = "48", MobileCountryCode = "260", PricePerSms = 0.032m });
        }
    }
}
