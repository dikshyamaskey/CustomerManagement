using CustomerManagement.Application.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CustomerManagement.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        public bool _disposed;

        private string _errorMessage = string.Empty;

        //The following Object is going to hold the Transaction Object
        private IDbContextTransaction _objTran;

        private ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext =  applicationDbContext;
            _objTran = CreateTransaction();


        }
        //The Dispose() method is used to free unmanaged resources like files, 
        //database connections etc. at any time.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

       
        //The CreateTransaction() method will create a database Transaction so that we can do database operations
        //by applying do everything and do nothing principle
        private IDbContextTransaction CreateTransaction()
        {
            //It will Begin the transaction on the underlying store connection
            _objTran = _applicationDbContext.Database.BeginTransaction();
            return _objTran;
        }

        //If all the Transactions are completed successfully then we need to call this Commit() 
        //method to Save the changes permanently in the database
        public void Commit()
        {
            //Commits the underlying store transaction
            _objTran.Commit();
        }

        //If at least one of the Transaction is Failed then we need to call this Rollback() 
        //method to Rollback the database changes to its previous state
        public void Rollback()
        {
            //Rolls back the underlying store transaction
            _objTran.Rollback();
            //The Dispose Method will clean up this transaction object and ensures Entity Framework
            //is no longer using that transaction.
            _objTran.Dispose();
        }

        //The CommitAsync() Method Implement DbContext Class SaveChanges method 
        //So whenever we do a transaction we need to call this CommitAsync() method 
        //so that it will make the changes in the database permanently
        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            //Calling DbContext Class SaveChanges method
            try
            {
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                await _objTran.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Rollback();
            }
           
        }

        //Disposing of the Context Object
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _applicationDbContext.Dispose();
            _disposed = true;
        }
    }
}
