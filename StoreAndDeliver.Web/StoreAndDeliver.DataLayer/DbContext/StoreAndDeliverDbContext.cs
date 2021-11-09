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
        public DbSet<Request> Requests { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<CargoRequest> CargoRequests { get; set; }
        public DbSet<CargoSetting> CargoSettings { get; set; }
        public DbSet<EnvironmentSetting> EnvironmentSettings { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<CargoSeesion> CargoSeesions { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Request>()
                .HasOne(r => r.FromAddress)
                .WithMany(r => r.RequestsFrom).HasForeignKey(r => r.FromAddressId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Request>()
                .HasOne(r => r.ToAddress)
                .WithMany(r => r.RequestsTo).HasForeignKey(r => r.ToAddressId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<AppUser>()
                .HasOne(c => c.Carrier)
                .WithOne(a => a.AppUser)
                .HasForeignKey<Carrier>(c => c.AppUserId);
            builder.Entity<Address>()
                .HasOne(a => a.Store)
                .WithOne(s => s.Address)
                .HasForeignKey<Store>(s => s.AddressId);

            base.OnModelCreating(builder);
        }
    }
}
