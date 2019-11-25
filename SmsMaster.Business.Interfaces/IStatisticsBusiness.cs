using SmsMaster.Model;
using SmsMaster.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmsMaster.Business.Interfaces
{
    public interface IStatisticsBusiness
    {
        Task<List<StatisticRecord>> GetStatistics(DateTime from, DateTime to, string mccList);
    }
}
