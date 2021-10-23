using StoreAndDeliver.BusinessLayer.Factories;
using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models.Auth;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.AuthorizationService
{
    public abstract class BaseAuthorizationService
    {
        private readonly IAuthTokenFactory _tokenFactory;
        public BaseAuthorizationService(IAuthTokenFactory tokenFactory)
        {
            _tokenFactory = tokenFactory;
        }
        public async Task<JWTTokenStatusResult> GenerateTokenAsync(AuthSignInModel model)
        {
            LoginErrorCodes status = await VerifyUserAsync(model);
            if (status != LoginErrorCodes.None)
            {
                return new JWTTokenStatusResult()
                {
                    Token = null,
                    IsAuthorized = false,
                    LoginErrorCode = status
                };
            }

            IEnumerable<Claim> claims = await GetUserClaimsAsync(model);
            JwtSecurityToken token = _tokenFactory.CreateToken(model.UserName.ToString(), claims);
            UserAuthInfo info = await GetUserInfoAsync(model.UserName);

            return new JWTTokenStatusResult()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                IsAuthorized = true,
                UserInfo = info,
                LoginErrorCode = LoginErrorCodes.None
            };
        }

        public abstract Task<IEnumerable<Claim>> GetUserClaimsAsync(AuthSignInModel model);
        public abstract Task<UserAuthInfo> GetUserInfoAsync(string userName);
        public abstract Task<LoginErrorCodes> VerifyUserAsync(AuthSignInModel model);
    }
}
