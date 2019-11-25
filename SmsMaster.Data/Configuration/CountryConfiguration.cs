using Microsoft.EntityFrameworkCore;
using SmsMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmsMaster.Data.Configuration
{
    public static class CountryConfiguration
    {
        public static void CountryMapping(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("nvarchar(30)");
                entity.Property(e => e.MobileCountryCode).HasColumnType("nvarchar(10)");
                entity.Property(e => e.CountryCode).HasColumnType("nvarchar(10)");
                entity.Property(e => e.PricePerSms).HasColumnType("decimal(6, 3)");

                entity.HasKey(e => e.MobileCountryCode);
            });
        }
    }
}
