using System;
using System.Collections.Generic;
using System.Text;

namespace SmsMaster.Model
{
    public class Sms
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public string Mcc { get; set; }
        public decimal Price { get; set; }
        public SmsState State { get; set; }
    }
}
