using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmsMaster.Business;
using SmsMaster.Data;
using SmsMaster.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmsMaster.Tests
{
    [TestClass]
    public class CountriesTest
    {
        //private  _manager;
        private SmsMasterContext _context;
        private UnitOfWork _uow;
        private CountriesBusiness _business;
        private List<Country> _countryList;
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


        public void Initialize()
        {
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
            var builder = new DbContextOptionsBuilder<SmsMasterContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseInternalServiceProvider(serviceProvider);
            _context = new SmsMasterContext(builder.Options);
            _countryList = GenerateCountries();
            _context.Country.AddRange(_countryList);
            _context.SaveChanges();
            _uow = new UnitOfWork(_context);
            _business = new CountriesBusiness(_uow);
        }

        [TestMethod]
        public async Task GetAllCountries()
        {
            Initialize();
            var business = new CountriesBusiness(_uow);
            var countries = await business.GetCountries();
            Assert.AreEqual(countries.Count, _countryList.Count);
        }
    }
}
