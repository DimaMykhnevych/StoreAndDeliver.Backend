using StoreAndDeliver.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmail(AppUser receiver, string url, string language);
        Task SendSuccessfullDeliveryEmail(Dictionary<AppUser, List<CargoRequest>> cargoRequests, string language);
    }
}
