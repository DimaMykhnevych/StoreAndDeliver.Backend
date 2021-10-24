using System.ComponentModel.DataAnnotations;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class CreateUserDto
    {
        public string Role { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Email { get; set; }
        public string ClientURIForEmailConfirmation { get; set; }
    }
}
