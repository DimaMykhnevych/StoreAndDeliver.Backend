using System;
using System.ComponentModel.DataAnnotations;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

        [MinLength(6)]
        public string Password { get; set; }
        [MinLength(6)]
        public string NewPassword { get; set; }
        [MinLength(6)]
        public string ConfirmPassword { get; set; }
    }
}
