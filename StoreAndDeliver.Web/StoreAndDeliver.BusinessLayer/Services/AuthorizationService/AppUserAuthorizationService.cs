using Microsoft.AspNetCore.Identity;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.Factories;
using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using StoreAndDeliver.DataLayer.Models.Auth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.AuthorizationService
{
    public class AppUserAuthorizationService : BaseAuthorizationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserAuthorizationService(
            IAuthTokenFactory tokenFactory,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
            : base(tokenFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public override async Task<IEnumerable<Claim>> GetUserClaimsAsync(AuthSignInModel model)
        {
            AppUser user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return new List<Claim> { };
            }

            return new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
                new Claim(AuthorizationConstants.ID, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };
        }

        public async override Task<LoginErrorCodes> VerifyUserAsync(AuthSignInModel model)
        {
            AppUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return LoginErrorCodes.InvalidUsernameOrPassword;
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return LoginErrorCodes.EmailConfirmationRequired;
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            return result.Succeeded ? LoginErrorCodes.None : LoginErrorCodes.InvalidUsernameOrPassword;
        }

        public async override Task<UserAuthInfo> GetUserInfoAsync(string userName)
        {
            if (userName == null) return null;
            AppUser user = await _userManager.FindByNameAsync(userName);

            UserAuthInfo info = new UserAuthInfo
            {
                Role = user.Role,
                UserId = user.Id,
                UserName = user.UserName,
                RegistryDate = user.RegistryDate,
                Email = user.Email
            };

            return info;
        }
    }
}
