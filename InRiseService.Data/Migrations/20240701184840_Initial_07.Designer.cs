﻿// <auto-generated />
using System;
using InRiseService.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InRiseService.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240701184840_Initial_07")]
    partial class Initial_07
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("PriceId")
                        .HasColumnType("int");

                    b.Property<string>("Refrigeration")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdateIn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("PriceId");

                    b.ToTable("Coolers");
                });

            modelBuilder.Entity("InRiseService.Domain.ImagesSite.ImagensProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CoolerId")
                        .HasColumnType("int");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("MemoryRamId")
                        .HasColumnType("int");

                    b.Property<int?>("MemoryRomId")
                        .HasColumnType("int");

                    b.Property<int?>("MonitorScreenId")
                        .HasColumnType("int");

                    b.Property<int?>("MotherBoardId")
                        .HasColumnType("int");

                    b.Property<string>("Pathkey")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("PowerSupplyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CoolerId");

                    b.HasIndex("MemoryRamId");

                    b.HasIndex("MemoryRomId");

                    b.HasIndex("MonitorScreenId");

                    b.HasIndex("MotherBoardId");

                    b.HasIndex("PowerSupplyId");

                    b.ToTable("ImagensProducts");
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

                    b.Property<int>("PriceId")
                        .HasColumnType("int");

                    b.Property<string>("Socket")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdateIn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("PriceId");

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

                    b.Property<int>("PriceId")
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

                    b.HasIndex("PriceId");

                    b.ToTable("MemoriesRom");
                });

            modelBuilder.Entity("InRiseService.Domain.MonitorsScreen.MonitorScreen", b =>
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("PriceId")
                        .HasColumnType("int");

                    b.Property<string>("Quality")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdateIn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UpdateVolume")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PriceId");

                    b.ToTable("MonitorsScreen");
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

                    b.Property<int>("PriceId")
                        .HasColumnType("int");

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

                    b.HasIndex("PriceId");

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
                        .HasColumnType("int");

                    b.Property<int>("PotencyReal")
                        .HasColumnType("int");

                    b.Property<int>("PriceId")
                        .HasColumnType("int");

                    b.Property<string>("Stamp")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdateIn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("PriceId");

                    b.ToTable("PowerSupplies");
                });

            modelBuilder.Entity("InRiseService.Domain.Prices.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("CostPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("FinalPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("IVA")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PorcentageADMCost")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PorcentageDiscount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PorcentageFixedCost")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PorcentageProfit")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Prices");
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

                    b.Property<int>("PriceId")
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

                    b.HasIndex("PriceId");

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

            modelBuilder.Entity("InRiseService.Domain.Coolers.Cooler", b =>
                {
                    b.HasOne("InRiseService.Domain.Prices.Price", "Price")
                        .WithMany()
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Price");
                });

            modelBuilder.Entity("InRiseService.Domain.ImagesSite.ImagensProduct", b =>
                {
                    b.HasOne("InRiseService.Domain.Coolers.Cooler", "Cooler")
                        .WithMany()
                        .HasForeignKey("CoolerId");

                    b.HasOne("InRiseService.Domain.MemoriesRam.MemoryRam", "MemoryRam")
                        .WithMany()
                        .HasForeignKey("MemoryRamId");

                    b.HasOne("InRiseService.Domain.MemoriesRom.MemoryRom", "MemoryRom")
                        .WithMany()
                        .HasForeignKey("MemoryRomId");

                    b.HasOne("InRiseService.Domain.MonitorsScreen.MonitorScreen", "MonitorScreen")
                        .WithMany()
                        .HasForeignKey("MonitorScreenId");

                    b.HasOne("InRiseService.Domain.MotherBoards.MotherBoard", "MotherBoard")
                        .WithMany()
                        .HasForeignKey("MotherBoardId");

                    b.HasOne("InRiseService.Domain.PowerSupplies.PowerSupply", "PowerSupply")
                        .WithMany()
                        .HasForeignKey("PowerSupplyId");

                    b.Navigation("Cooler");

                    b.Navigation("MemoryRam");

                    b.Navigation("MemoryRom");

                    b.Navigation("MonitorScreen");

                    b.Navigation("MotherBoard");

                    b.Navigation("PowerSupply");
                });

            modelBuilder.Entity("InRiseService.Domain.MemoriesRam.MemoryRam", b =>
                {
                    b.HasOne("InRiseService.Domain.Prices.Price", "Price")
                        .WithMany()
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Price");
                });

            modelBuilder.Entity("InRiseService.Domain.MemoriesRom.MemoryRom", b =>
                {
                    b.HasOne("InRiseService.Domain.Prices.Price", "Price")
                        .WithMany()
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Price");
                });

            modelBuilder.Entity("InRiseService.Domain.MonitorsScreen.MonitorScreen", b =>
                {
                    b.HasOne("InRiseService.Domain.Prices.Price", "Price")
                        .WithMany()
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Price");
                });

            modelBuilder.Entity("InRiseService.Domain.MotherBoards.MotherBoard", b =>
                {
                    b.HasOne("InRiseService.Domain.Prices.Price", "Price")
                        .WithMany()
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Price");
                });

            modelBuilder.Entity("InRiseService.Domain.PowerSupplies.PowerSupply", b =>
                {
                    b.HasOne("InRiseService.Domain.Prices.Price", "Price")
                        .WithMany()
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Price");
                });

            modelBuilder.Entity("InRiseService.Domain.Processors.Processor", b =>
                {
                    b.HasOne("InRiseService.Domain.Prices.Price", "Price")
                        .WithMany()
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Price");
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
