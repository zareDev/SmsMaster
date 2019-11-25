using Newtonsoft.Json;
using SmsMaster.Data.Interfaces;
using SmsMaster.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmsMaster.Data
{
    public class SmsServiceAdapter : ISmsServiceAdapter
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _sendSmsUrl;

        public SmsServiceAdapter(string serviceUrl, string sendSmsUrl)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(serviceUrl) };
            _sendSmsUrl = sendSmsUrl;
        }

        public async Task<SmsState> SendSms(Sms entity)
        {
            try
            {
                var myContent = JsonConvert.SerializeObject(entity);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //string url = _sendSmsUrl + $"?Id={entity.Id}&From={entity.From}&To={entity.To}&Text={entity.Text}&DateTime={entity.DateTime}&Mcc={entity.Mcc}&Price={entity.Price}&State={entity.State}";
                //var response = await _httpClient.GetStringAsync(url);
                var response = await _httpClient.PostAsync(_sendSmsUrl, byteContent);
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<SmsState>(result);
                }
                return SmsState.Failed;
            }
            catch (Exception ex)
            {
                return SmsState.Failed;
            }
        }
    }
}
