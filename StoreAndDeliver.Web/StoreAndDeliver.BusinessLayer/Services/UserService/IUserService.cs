using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.UserService
{
    public interface IUserService
    {
        Task<AppUser> GetUserByUsername(string username);
        Task<AppUser> CreateUserAsync(CreateUserDto userModel);
        Task DeleteUser(Guid userId);
        Task<ConfirmEmailDto> ConfirmEmail(ConfirmEmailDto confirmEmailDto);
    }
}
