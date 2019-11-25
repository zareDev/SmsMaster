using System;
using System.Collections.Generic;
using System.Text;

namespace SmsMaster.Model.DTO
{
    public class StatisticRecord
    {
        public DateTime Day { get; set; }
        public string Mcc { get; set; }
        public decimal PricePerSms { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
