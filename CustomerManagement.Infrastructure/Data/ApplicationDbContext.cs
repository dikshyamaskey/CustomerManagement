
using CustomerManagement.Application.Interface;
using CustomerManagement.Core.Entities;
using CustomerManagement.Infrastructure.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IMediator _mediator;

        public ApplicationDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

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
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);
            return await base.SaveChangesAsync(cancellationToken);
        }


    }
}
