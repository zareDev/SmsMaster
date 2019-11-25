using SmsMaster.Data.Interfaces;
using SmsMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SmsMaster.Data
{
    public class CountriesRepository: GenericRepository<Country>, ICountriesRepository
    {
        public CountriesRepository(SmsMasterContext context):base(context)
        {
        }


        public async Task<Country> GetByCountryCodeAsync(string countryCode)
        {
            return await FindAsync(c => c.CountryCode == countryCode);
        }
    }
}
