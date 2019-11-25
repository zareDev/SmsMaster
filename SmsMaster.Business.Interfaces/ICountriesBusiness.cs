using SmsMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmsMaster.Business.Interfaces
{
    public interface ICountriesBusiness
    {
        Task<List<Country>> GetCountries();
    }
}
