using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmsMaster.Business.Interfaces;
using SmsMaster.Data;
using SmsMaster.Data.Interfaces;
using SmsMaster.DTO;
using SmsMaster.Model;
using SmsMaster.Model.DTO;

namespace SmsMaster.Controllers
{
    [Route("api/sms")]
    [ApiController]
    public class SmsController : Controller
    {
        private readonly ISmsBusiness _business;
        private readonly IMapper _mapper;
        public SmsController(ISmsBusiness business, IMapper mapper)
        {
            _mapper = mapper;
            _business = business;
        }

        [HttpGet("send.{format}"), FormatFilter]
        public async Task<SmsState> SendJSON([FromQuery]SendSmsRequest req)
        {
            var sms = _mapper.Map<Model.Sms>(req);
            return await _business.SendSms(sms);
        }

        [HttpGet("sent.{format}"), FormatFilter]
        public async Task<QueryResult<Sms>> GetSentSmsJSON([FromQuery]SmsQuery req)
        {
            //var sms = _mapper.Map<Model.Sms>(req);
            return await _business.GetSentSms(req);
        }
    }
}