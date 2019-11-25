using System.Threading.Tasks;
using SmsMaster.Model;

namespace SmsMaster.Data.Interfaces
{
    public interface ISmsServiceAdapter
    {
        Task<SmsState> SendSms(Sms entity);
    }
}