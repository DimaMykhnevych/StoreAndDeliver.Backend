using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.Models;
using System;

namespace StoreAndDeliver.DataLayer.DbContext
{
    public class StoreAndDeliverDbContext : IdentityDbContext<AppUser, UserRole, Guid>
    {
        public StoreAndDeliverDbContext(DbContextOptions<StoreAndDeliverDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
