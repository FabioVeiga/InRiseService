using InRiseService.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace InRiseService.Data.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

        DbSet<User> Users { get; set; }
    }
}