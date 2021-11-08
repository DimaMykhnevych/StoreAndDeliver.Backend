using StoreAndDeliver.DataLayer.Enums;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.ConvertionService
{
    public interface IConvertionService
    {
        Task<decimal> ConvertCurrency(string from, string to, decimal amount);
        double ConvertWeigth(WeightUnit from, WeightUnit to, double value);
        double ConvertLength(LengthUnit from, LengthUnit to, double value);
        double ConvertTemperature(TemperatureUnit from, TemperatureUnit to, double value);
    }
}
