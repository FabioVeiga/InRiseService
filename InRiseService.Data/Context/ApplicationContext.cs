using InRiseService.Domain.Addressed;
using InRiseService.Domain.Categories;
using InRiseService.Domain.MotherBoards;
using InRiseService.Domain.Processors;
using InRiseService.Domain.Users;
using InRiseService.Domain.UsersAddress;
using InRiseService.Domain.ValidationCodes;
using Microsoft.EntityFrameworkCore;

namespace InRiseService.Data.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ValidationCode> ValidationCodes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Processor> Processors { get; set; }
        public DbSet<MotherBoard> MotherBoards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}