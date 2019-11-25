using SmsMaster.Business.Interfaces;
using SmsMaster.Data.Interfaces;
using SmsMaster.Model;
using SmsMaster.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmsMaster.Business
{
    public class SmsBusiness : ISmsBusiness
    {
        private readonly IUnitOfWork _uow;
        private readonly ISmsServiceAdapter _smsAdapter;

        public SmsBusiness(IUnitOfWork uow, ISmsServiceAdapter smsAdapter)
        {
            _uow = uow;
            _smsAdapter = smsAdapter;
        }

        public async Task<QueryResult<Sms>> GetSentSms(SmsQuery smsQuery)
        {
            smsQuery.SortBy = "DateTime";
            smsQuery.IsSortAscending = true;
            return await _uow.Sms.GetPagedAsync(smsQuery);
        }

        public async Task<SmsState> SendSms(Sms sms)
        {
            if (!ValidateMobilePhone(sms.To))
                return SmsState.Failed;

            Country country = await GetCountryForSmsAsync(sms);
            if (country == null) return SmsState.Failed;

            sms.State = SmsState.Failed;
            sms.DateTime = DateTime.UtcNow;
            sms.Mcc = country.MobileCountryCode;
            sms.Price = country.PricePerSms;

            _uow.Sms.Add(sms);
            _uow.Save();
            sms.State = await _smsAdapter.SendSms(sms);
            _uow.Save();
            return sms.State;
        }

        private async Task<Country> GetCountryForSmsAsync(Sms sms)
        {
            string countryCode = sms.To.Substring(1, 2);
            return await _uow.Countries.GetByCountryCodeAsync(countryCode);
        }

        private bool ValidateMobilePhone(string phone)
        {
            Regex regex = new Regex(@"\+\d{13}");
            Match match = regex.Match(phone);
            if (match.Success) return true;
            return false;
        }
    }
}
