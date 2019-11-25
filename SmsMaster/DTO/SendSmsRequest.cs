using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmsMaster.DTO
{
    public class SendSmsRequest
    {
        public string From { get; set; }
        //[RegularExpression(@"^\+d{13}")]
        public string To { get;set; }
        public string Text { get;set; }
    }
}
