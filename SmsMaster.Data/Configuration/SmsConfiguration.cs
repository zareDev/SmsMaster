using Microsoft.EntityFrameworkCore;
using SmsMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmsMaster.Data.Configuration
{
    public static class SmsConfiguration
    {
        public static void SmsMapping(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sms>(entity =>
            {
                entity.Property(e => e.From).HasColumnType("nvarchar(100)");
                entity.Property(e => e.To).HasColumnType("nvarchar(20)");
                entity.Property(e => e.Text).HasColumnType("nvarchar(500)");
                entity.Property(e => e.Mcc).HasColumnType("nvarchar(10)");
                entity.Property(e => e.Price).HasColumnType("decimal(6, 3)");

                entity.HasKey(e => e.Id);


            });
        }
    }
}
