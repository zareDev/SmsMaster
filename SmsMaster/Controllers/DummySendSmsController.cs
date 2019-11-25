using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmsMaster.Model;

namespace SmsMaster.Controllers
{
    [Route("api/DummySendSms")]
    [ApiController]
    public class DummySendSmsController : ControllerBase
    {
        private readonly ILogger<DummySendSmsController> _logger;
        public DummySendSmsController(ILogger<DummySendSmsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public SmsState SendSms([FromBody]Sms sms)
        {
            _logger.LogInformation($"SMS From:{sms.From} To{sms.To} Text {sms.Text}");
            //Random random = new Random();
            //if (random.Next(100) < 50)
            //    return SmsState.Failed;
            return SmsState.Success;
        }
    }
}