using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Application.Interface
{
    public interface IUnitofWorkFactory
    {
        IUnitOfWork CreateUnitOfWork();
    }

}
