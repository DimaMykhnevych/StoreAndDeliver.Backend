using StoreAndDeliver.BusinessLayer.DTOs;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.RequestService
{
    public interface IRequestService
    {
        Task<RequestDto> AddRequest(AddRequestDto addRequestDto);
        Task<decimal> CalculateRequestPrice(AddRequestDto requestAddDto);
    }
}
