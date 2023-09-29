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
        public DbSet<Core.Entities.User> User { get; }
        public DbSet<Core.Entities.Employee> Employee { get;  }
        public DbSet<Core.Entities.Customer> Customer { get; }
    }
}
