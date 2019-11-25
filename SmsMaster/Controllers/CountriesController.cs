using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmsMaster.Business.Interfaces;
using SmsMaster.Data;
using SmsMaster.Data.Interfaces;
using SmsMaster.Model;

namespace SmsMaster.Controllers
{
    [Route("api")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesBusiness _business;
        public CountriesController(ICountriesBusiness business)
        {
            _business = business;
        }

        [HttpGet("countries.{format}"), FormatFilter]
        public async Task<List<Country>> GetJSON()
        {
            return await _business.GetCountries();
        }
    }
}