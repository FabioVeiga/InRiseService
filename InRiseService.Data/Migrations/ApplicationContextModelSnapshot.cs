﻿// <auto-generated />
using System;
using InRiseService.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InRiseService.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("InRiseService.Domain.Addressed.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("DeleteIn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("InsertIn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdateIn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("InRiseService.Domain.Coolers.Cooler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Air")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("DeleteIn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Dimension")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<int>("FanDiametric")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<int>("FanQuantity")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertIn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MaxVelocit")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Refrigeration")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdateIn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Coolers");
                });

            modelBuilder.Entity("InRiseService.Domain.MemoriesRam.MemoryRam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteIn")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("Frequency")
                        .HasColumnType("double");

                    b.Property<DateTime>("InsertIn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Socket")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdateIn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("MemoriesRam");
                });

            modelBuilder.Entity("InRiseService.Domain.MemoriesRom.MemoryRom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteIn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("InsertIn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Potency")
                        .HasColumnType("int");

                    b.Property<string>("Socket")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdateIn")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("VelocityRead")
                        .HasColumnType("double");

                    b.Property<double>("VelocityWrite")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("MemoriesRom");
                });

            modelBuilder.Entity("InRiseService.Domain.MotherBoards.MotherBoard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("DeleteIn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("InsertIn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Socket")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SocketM2")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SocketMemory")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SocketMemoryVideo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SocketSSD")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdateIn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("MotherBoards");
                });

            modelBuilder.Entity("InRiseService.Domain.PowerSupplies.PowerSupply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("DeleteIn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("InsertIn")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Modular")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Potency")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<int>("PotencyReal")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<string>("Stamp")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdateIn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("PowerSupplies");
                });

            modelBuilder.Entity("InRiseService.Domain.Processors.Processor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Core")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteIn")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("Frequency")
                        .HasMaxLength(100)
                        .HasColumnType("double");

                    b.Property<string>("Generation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("InsertIn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Potency")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<string>("Socket")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SuportMemoryRAM")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SuportMemoryROM")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SuportVideo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdateIn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Processors");
                });

            modelBuilder.Entity("InRiseService.Domain.Towers.Tower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("DeleteIn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Dimesion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("InsertIn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MaxFans")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdateIn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Towers");
                });

            modelBuilder.Entity("InRiseService.Domain.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("DeleteIn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailValide")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("InsertIn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("Marketing")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberValide")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Profile")
                        .HasColumnType("int");

                    b.Property<bool>("Term")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("UpdateIn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("InRiseService.Domain.UsersAddress.UserAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteIn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("InsertIn")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsBilling")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Observation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdateIn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAddresses");
                });

            modelBuilder.Entity("InRiseService.Domain.ValidationCodes.ValidationCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteIn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExpirateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("InsertIn")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsValidate")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("TypeCode")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateIn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ValidationCodes");
                });

            modelBuilder.Entity("InRiseService.Domain.VideoBoards.VideoBoard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Bits")
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteIn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Dimension")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("InsertIn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Potency")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<string>("Socket")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdateIn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("VideosBoard");
                });

            modelBuilder.Entity("InRiseService.Domain.UsersAddress.UserAddress", b =>
                {
                    b.HasOne("InRiseService.Domain.Addressed.Address", "Address")
                        .WithMany("UserAddresses")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InRiseService.Domain.Users.User", "User")
                        .WithMany("Address")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("InRiseService.Domain.ValidationCodes.ValidationCode", b =>
                {
                    b.HasOne("InRiseService.Domain.Users.User", "User")
                        .WithOne("ValidationCode")
                        .HasForeignKey("InRiseService.Domain.ValidationCodes.ValidationCode", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InRiseService.Domain.Addressed.Address", b =>
                {
                    b.Navigation("UserAddresses");
                });

            modelBuilder.Entity("InRiseService.Domain.Users.User", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("ValidationCode")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
