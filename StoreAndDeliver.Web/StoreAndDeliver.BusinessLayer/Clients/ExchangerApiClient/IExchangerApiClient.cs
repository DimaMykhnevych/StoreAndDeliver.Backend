using StoreAndDeliver.BusinessLayer.DTOs;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Clients.ExchangerApiClient
{
    public interface IExchangerApiClient
    {
        Task<ExchangerApiConvertionDto> GetCurrencyRate();
    }
}
