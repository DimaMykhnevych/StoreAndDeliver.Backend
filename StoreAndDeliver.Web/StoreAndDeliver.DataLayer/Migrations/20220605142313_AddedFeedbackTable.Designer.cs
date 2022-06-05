﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreAndDeliver.DataLayer.DbContext;

namespace StoreAndDeliver.DataLayer.Migrations
{
    [DbContext(typeof(StoreAndDeliverDbContext))]
    [Migration("20220605142313_AddedFeedbackTable")]
    partial class AddedFeedbackTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<double>("Latitude")
                        .HasColumnType("double");

                    b.Property<double>("Longtitude")
                        .HasColumnType("double");

                    b.Property<string>("Street")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("RegistryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.Cargo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<double>("Height")
                        .HasColumnType("double");

                    b.Property<double>("Length")
                        .HasColumnType("double");

                    b.Property<double>("Weight")
                        .HasColumnType("double");

                    b.Property<double>("Width")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Cargo");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.CargoRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CargoId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RequestId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("StoreId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("RequestId");

                    b.HasIndex("StoreId");

                    b.ToTable("CargoRequests");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.CargoSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CargoRequestId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CarrierId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CargoRequestId");

                    b.HasIndex("CarrierId");

                    b.ToTable("CargoSessions");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.CargoSessionNote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CargoSessionId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("NoteCreationDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CargoSessionId");

                    b.ToTable("CargoSessionNotes");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.CargoSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CargoId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("EnvironmentSettingId")
                        .HasColumnType("char(36)");

                    b.Property<double>("MaxValue")
                        .HasColumnType("double");

                    b.Property<double>("MinValue")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("EnvironmentSettingId");

                    b.ToTable("CargoSettings");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.CargoSnapshot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CargoSessionId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("EnvironmentSettingId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Value")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("CargoSessionId");

                    b.HasIndex("EnvironmentSettingId");

                    b.ToTable("CargoSnapshots");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.Carrier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("longtext");

                    b.Property<double>("CurrentOccupiedVolume")
                        .HasColumnType("double");

                    b.Property<double>("MaxCargoVolume")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId")
                        .IsUnique();

                    b.ToTable("Carriers");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CityName")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<double>("Latitude")
                        .HasColumnType("double");

                    b.Property<double>("Longtitude")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.EnvironmentSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("EnvironmentSettings");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("Rating")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.Request", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CarryOutBefore")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("FromAddressId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsSecurityModeEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("StoreFromDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("StoreUntilDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("ToAddressId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("TotalSum")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("FromAddressId");

                    b.HasIndex("ToAddressId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.Store", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("char(36)");

                    b.Property<double>("CurrentOccupiedVolume")
                        .HasColumnType("double");

                    b.Property<double>("MaxCargoVolume")
                        .HasColumnType("double");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("StoreAndDeliver.DataLayer.Models.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("StoreAndDeliver.DataLayer.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("StoreAndDeliver.DataLayer.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("StoreAndDeliver.DataLayer.Models.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreAndDeliver.DataLayer.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("StoreAndDeliver.DataLayer.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.CargoRequest", b =>
                {
                    b.HasOne("StoreAndDeliver.DataLayer.Models.Cargo", "Cargo")
                        .WithMany("CargoRequests")
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreAndDeliver.DataLayer.Models.Request", "Request")
                        .WithMany("CargoRequests")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreAndDeliver.DataLayer.Models.Store", "Store")
                        .WithMany("CargoRequests")
                        .HasForeignKey("StoreId");

                    b.Navigation("Cargo");

                    b.Navigation("Request");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.CargoSession", b =>
                {
                    b.HasOne("StoreAndDeliver.DataLayer.Models.CargoRequest", "CargoRequest")
                        .WithMany()
                        .HasForeignKey("CargoRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreAndDeliver.DataLayer.Models.Carrier", "Carrier")
                        .WithMany("CargoSeesions")
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CargoRequest");

                    b.Navigation("Carrier");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.CargoSessionNote", b =>
                {
                    b.HasOne("StoreAndDeliver.DataLayer.Models.CargoSession", "CargoSession")
                        .WithMany("CargoSessionNotes")
                        .HasForeignKey("CargoSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CargoSession");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.CargoSetting", b =>
                {
                    b.HasOne("StoreAndDeliver.DataLayer.Models.Cargo", "Cargo")
                        .WithMany("CargoSettings")
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreAndDeliver.DataLayer.Models.EnvironmentSetting", "EnvironmentSetting")
                        .WithMany("CargoSettings")
                        .HasForeignKey("EnvironmentSettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("EnvironmentSetting");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.CargoSnapshot", b =>
                {
                    b.HasOne("StoreAndDeliver.DataLayer.Models.CargoSession", "CargoSession")
                        .WithMany("CargoSnapshots")
                        .HasForeignKey("CargoSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreAndDeliver.DataLayer.Models.EnvironmentSetting", "EnvironmentSetting")
                        .WithMany("CargoSnapshots")
                        .HasForeignKey("EnvironmentSettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CargoSession");

                    b.Navigation("EnvironmentSetting");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.Carrier", b =>
                {
                    b.HasOne("StoreAndDeliver.DataLayer.Models.AppUser", "AppUser")
                        .WithOne("Carrier")
                        .HasForeignKey("StoreAndDeliver.DataLayer.Models.Carrier", "AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.Feedback", b =>
                {
                    b.HasOne("StoreAndDeliver.DataLayer.Models.AppUser", "AppUser")
                        .WithMany("Feedback")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.Request", b =>
                {
                    b.HasOne("StoreAndDeliver.DataLayer.Models.AppUser", "AppUser")
                        .WithMany("Requests")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreAndDeliver.DataLayer.Models.Address", "FromAddress")
                        .WithMany("RequestsFrom")
                        .HasForeignKey("FromAddressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("StoreAndDeliver.DataLayer.Models.Address", "ToAddress")
                        .WithMany("RequestsTo")
                        .HasForeignKey("ToAddressId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("AppUser");

                    b.Navigation("FromAddress");

                    b.Navigation("ToAddress");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.Store", b =>
                {
                    b.HasOne("StoreAndDeliver.DataLayer.Models.Address", "Address")
                        .WithOne("Store")
                        .HasForeignKey("StoreAndDeliver.DataLayer.Models.Store", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.Address", b =>
                {
                    b.Navigation("RequestsFrom");

                    b.Navigation("RequestsTo");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.AppUser", b =>
                {
                    b.Navigation("Carrier");

                    b.Navigation("Feedback");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.Cargo", b =>
                {
                    b.Navigation("CargoRequests");

                    b.Navigation("CargoSettings");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.CargoSession", b =>
                {
                    b.Navigation("CargoSessionNotes");

                    b.Navigation("CargoSnapshots");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.Carrier", b =>
                {
                    b.Navigation("CargoSeesions");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.EnvironmentSetting", b =>
                {
                    b.Navigation("CargoSettings");

                    b.Navigation("CargoSnapshots");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.Request", b =>
                {
                    b.Navigation("CargoRequests");
                });

            modelBuilder.Entity("StoreAndDeliver.DataLayer.Models.Store", b =>
                {
                    b.Navigation("CargoRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
