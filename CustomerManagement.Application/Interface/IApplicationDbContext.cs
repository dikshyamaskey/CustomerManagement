using CustomerManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Application.Interface
{
    public interface IApplicationDbContext
    {
        public DbSet<User> User { get; }
        public DbSet<Employee> Employee { get;  }
        public DbSet<Customer> Customer { get; }
    }
}
