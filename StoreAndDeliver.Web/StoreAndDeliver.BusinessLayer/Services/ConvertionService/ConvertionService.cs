using StoreAndDeliver.BusinessLayer.Clients.ExchangerApiClient;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.ConvertionService
{
    public class ConvertionService : IConvertionService
    {
        private readonly IExchangerApiClient _exchangerApiClient;

        private readonly Dictionary<WeightUnit, double> _weightDictionary =
            new()
            {
                { WeightUnit.Kilograms, 1 },
                { WeightUnit.Pounds, 0.453592 }
            };

        private readonly Dictionary<LengthUnit, double> _lengthDictionary =
            new()
            {
                { LengthUnit.Meters, 1 },
                { LengthUnit.Yards, 0.9144 }
            };

        public ConvertionService(IExchangerApiClient exchangerApiClient)
        {
            _exchangerApiClient = exchangerApiClient;
        }

        public async Task<decimal> ConvertCurrency(string from, string to, decimal amount)
        {
            ExchangerApiConvertionDto response =  await _exchangerApiClient.GetCurrencyRate();

            Dictionary<string, decimal> currencyDictionary = new()
            {
                { Currency.Uah, response.Rates.UAH},
                { Currency.Rub, response.Rates.RUB },
                { Currency.Usd, response.Rates.USD },
            };

            if (response.Success)
            {
                return (currencyDictionary[to] / currencyDictionary[from]) * amount;
            }
            throw new Exception("Currency convertion failed");
        }

        public double ConvertTemperature(TemperatureUnit from, TemperatureUnit to, double value)
        {
            if (from == to)
            {
                return value;
            }
            Dictionary<TemperatureUnit, Dictionary<TemperatureUnit, double>> tempDictionary = new()
            {
                { TemperatureUnit.Fahrenheit, new() },
                { TemperatureUnit.Celsius, new() },
                { TemperatureUnit.Kelvin, new() }

            };
            switch (from) {
                case TemperatureUnit.Celsius:
                    tempDictionary[TemperatureUnit.Celsius]
                        .Add(TemperatureUnit.Fahrenheit, value * 9 / 5 + 32);
                    tempDictionary[TemperatureUnit.Celsius]
                        .Add(TemperatureUnit.Kelvin, value + 273.15);
                    break;
                case TemperatureUnit.Fahrenheit:
                    tempDictionary[TemperatureUnit.Fahrenheit]
                        .Add(TemperatureUnit.Celsius, (value - 32) * 5 / 9);
                    tempDictionary[TemperatureUnit.Fahrenheit]
                        .Add(TemperatureUnit.Kelvin, ((value - 32) * 5 / 9) + 273.15);
                    break;
                case TemperatureUnit.Kelvin:
                    tempDictionary[TemperatureUnit.Kelvin]
                       .Add(TemperatureUnit.Fahrenheit, ((value - 273.15) * 9 / 5) + 32);
                    tempDictionary[TemperatureUnit.Kelvin]
                        .Add(TemperatureUnit.Celsius, value - 273.15);
                    break;
            }
            return tempDictionary[from][to];
        }

        public double ConvertLength(LengthUnit from, LengthUnit to, double value)
        {
            double coefficient = _lengthDictionary[from] / _lengthDictionary[to];
            return coefficient * value;
        }

        public double ConvertWeigth(WeightUnit from, WeightUnit to, double value)
        {
            double coefficient = _weightDictionary[from] / _weightDictionary[to];
            return coefficient * value;
        }
    }
}
