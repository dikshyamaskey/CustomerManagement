using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CustomerManagement.Application.Interface
{
    public interface IUnitOfWork :IDisposable
    {

        //Commit the database Transaction
        void Commit();
        //Rollback the database Transaction
        void Rollback();
        //DbContext Class SaveChanges method
        Task CommitAsync();
    }
}
