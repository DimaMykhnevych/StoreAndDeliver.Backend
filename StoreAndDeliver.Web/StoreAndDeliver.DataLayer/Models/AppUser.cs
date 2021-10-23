using Microsoft.AspNetCore.Identity;
using System;

namespace StoreAndDeliver.DataLayer.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Role { get; set; }
        public DateTime RegistryDate { get; set; }
    }
}
