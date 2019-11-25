using System;

namespace SmsMaster.Model
{
    public class Country
    {
        public string Name { get; set; }
        public string MobileCountryCode { get; set; }
        public string CountryCode { get; set; }
        public decimal PricePerSms { get; set; }
    }
}
