using System;
using System.Collections.Generic;
using System.Text;

namespace SmsMaster.Model.DTO
{
    public class SmsQuery : QueryObject
    {
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
    }
}
