using SmsMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmsMaster.Data.Interfaces
{
    public interface ICountriesRepository: IGenericRepository<Country>
    {
        Task<Country> GetByCountryCodeAsync(string countryCode);
    }
}
