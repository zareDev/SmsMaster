using SmsMaster.Business.Interfaces;
using SmsMaster.Data.Interfaces;
using SmsMaster.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmsMaster.Business
{
    public class CountriesBusiness : ICountriesBusiness
    {
        private readonly IUnitOfWork _uow;

        public CountriesBusiness(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<List<Country>> GetCountries()
        {
            return await _uow.Countries.GetAllAsync();
        }
    }
}
