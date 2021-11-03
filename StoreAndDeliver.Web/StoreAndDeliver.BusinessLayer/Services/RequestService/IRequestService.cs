using StoreAndDeliver.BusinessLayer.DTOs;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.RequestService
{
    public interface IRequestService
    {
        Task<decimal> CalculateRequestPrice(AddRequestDto requestAddDto);
    }
}
