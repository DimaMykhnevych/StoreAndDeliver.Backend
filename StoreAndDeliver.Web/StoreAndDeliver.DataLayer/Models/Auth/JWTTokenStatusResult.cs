using StoreAndDeliver.DataLayer.Enums;

namespace StoreAndDeliver.DataLayer.Models.Auth
{
    public class JWTTokenStatusResult
    {
        public string Token { get; set; }
        public bool IsAuthorized { get; set; }
        public UserAuthInfo UserInfo { get; set; }
        public LoginErrorCodes LoginErrorCode { get; set; }
    }
}
