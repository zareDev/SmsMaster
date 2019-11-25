using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmsMaster.Business.Interfaces;
using SmsMaster.Model.DTO;

namespace SmsMaster.Controllers
{
    [Route("api")]
    [ApiController]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsBusiness _business;

        public StatisticsController(IStatisticsBusiness business)
        {
            _business = business;
        }

        [HttpGet("statistics.{format}"), FormatFilter]
        public async Task<List<StatisticRecord>> GetStatistics(DateTime dateFrom, DateTime dateTo, string mccList)
        {
            return await _business.GetStatistics(dateFrom, dateTo, mccList);
        }
    }
}