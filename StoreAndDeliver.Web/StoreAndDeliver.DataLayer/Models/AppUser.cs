using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace StoreAndDeliver.DataLayer.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Role { get; set; }
        public DateTime RegistryDate { get; set; }

        public ICollection<Request> Requests { get; set; }
        public virtual Carrier Carrier { get; set; }
    }
}
