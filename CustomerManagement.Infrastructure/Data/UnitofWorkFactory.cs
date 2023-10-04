using CustomerManagement.Application.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Infrastructure.Data
{
    public class UnitofWorkFactory :IUnitofWorkFactory
    {
        private ApplicationDbContext _applicationDbContext;
        public UnitofWorkFactory (ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(_applicationDbContext);
        }

      
    }
}
