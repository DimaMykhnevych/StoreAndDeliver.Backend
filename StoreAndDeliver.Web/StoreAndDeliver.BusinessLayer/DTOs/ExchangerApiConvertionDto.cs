using System;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class ExchangerApiConvertionDto
    {
        public bool Success { get; set; }
        public long Timestamp { get; set; }
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public CurrencyRates Rates { get; set; }
    }

    public class CurrencyRates
    {
        public decimal USD { get; set; }
        public decimal UAH { get; set; }
        public decimal RUB { get; set; }

    }
}
