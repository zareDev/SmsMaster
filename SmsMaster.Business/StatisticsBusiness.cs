using SmsMaster.Business.Interfaces;
using SmsMaster.Data.Interfaces;
using SmsMaster.Model;
using SmsMaster.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsMaster.Business
{
    public class StatisticsBusiness : IStatisticsBusiness
    {
        //private readonly ICountriesRepository _countriesRepository;
        private readonly IUnitOfWork _uow;

        public StatisticsBusiness(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<StatisticRecord>> GetStatistics(DateTime from, DateTime to, string mccList)
        {
            var retVal = new List<StatisticRecord>();
            DateTime startDate = new DateTime(from.Year, from.Month, from.Day);
            DateTime endDate = new DateTime(to.Year, to.Month, to.Day);

            List<Sms> queryResult;

            string[] mccListArray;
            if (!string.IsNullOrEmpty(mccList))
            {
                mccListArray = mccList.Split(',');
                queryResult = await _uow.Sms.GetAllAsync(e => e.DateTime >= from && e.DateTime < endDate.AddDays(1) && mccListArray.Contains(e.Mcc));
            }
            else
            {
                mccListArray = (await _uow.Countries.GetAllAsync()).Select(e=>e.MobileCountryCode).ToArray();
                queryResult = await _uow.Sms.GetAllAsync();
            }

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                foreach (var mcc in mccListArray)
                {
                    var recordResults = queryResult.Where(e => e.DateTime >= date && e.DateTime < date.AddDays(1) && e.Mcc == mcc);
                    var totalPrice = recordResults.Sum(e => e.Price);
                    var count = recordResults.Count();
                    StatisticRecord record = new StatisticRecord
                    {
                        Day = date,
                        Mcc = mcc,
                        TotalPrice = totalPrice,
                        Count = count,
                        PricePerSms = count == 0 ? 0 : totalPrice / count
                    };
                    retVal.Add(record);
                }
            }

            return retVal;
        }
    }
}
