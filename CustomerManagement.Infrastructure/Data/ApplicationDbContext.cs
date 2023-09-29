
using CustomerManagement.Application.Interface;
using CustomerManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> User=>Set<User>();
        public DbSet<Employee> Employee => Set<Employee>();
        public DbSet<Customer> Customer => Set<Customer>();
        public DbSet<Membership> Membership => Set<Membership>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.Entity<Customer>().HasIndex(x => x.UserId).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(x => x.UserId).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.EmailAddress).IsUnique();
            base.OnModelCreating(modelBuilder);
           
        }

    }
}
