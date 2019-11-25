using SmsMaster.Model;
using SmsMaster.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmsMaster.Business.Interfaces
{
    public interface ISmsBusiness
    {
        Task<SmsState> SendSms(Sms sms);
        Task<QueryResult<Sms>> GetSentSms(SmsQuery smsQuery);
    }
}
