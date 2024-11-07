using InRiseService.Domain.Addressed;
using InRiseService.Domain.Categories;
using InRiseService.Domain.Computers;
using InRiseService.Domain.Coolers;
using InRiseService.Domain.ImagesSite;
using InRiseService.Domain.LandingPages;
using InRiseService.Domain.MemoriesRam;
using InRiseService.Domain.MemoriesRom;
using InRiseService.Domain.MonitorsScreen;
using InRiseService.Domain.MotherBoards;
using InRiseService.Domain.Orders;
using InRiseService.Domain.OrderStatuses;
using InRiseService.Domain.PowerSupplies;
using InRiseService.Domain.Prices;
using InRiseService.Domain.Processors;
using InRiseService.Domain.Softwares;
using InRiseService.Domain.Towers;
using InRiseService.Domain.Users;
using InRiseService.Domain.UsersAddress;
using InRiseService.Domain.ValidationCodes;
using InRiseService.Domain.VideoBoards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InRiseService.Data.Context
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ValidationCode> ValidationCodes { get; set; }
        public DbSet<Processor> Processors { get; set; }
        public DbSet<MotherBoard> MotherBoards { get; set; }
        public DbSet<MemoryRam> MemoriesRam { get; set; }
        public DbSet<MemoryRom> MemoriesRom { get; set; }
        public DbSet<VideoBoard> VideosBoard { get; set; }
        public DbSet<PowerSupply> PowerSupplies { get; set; }
        public DbSet<Cooler> Coolers { get; set; }
        public DbSet<Tower> Towers { get; set; }
        public DbSet<MonitorScreen> MonitorsScreen { get; set; }
        public DbSet<ImagensProduct> ImagensProducts { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<LandingPage> LandingPages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<OrderHistoric> OrderHistorics { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Software> Softwares { get; set; }

    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23)));

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}