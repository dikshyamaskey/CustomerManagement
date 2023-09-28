namespace CustomerManagement.Infrastructure.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
         IQueryable<TEntity> Table();
         IQueryable<TEntity> TableNoTracking();

        TEntity Insert(TEntity entity);
         void  Update(TEntity entity);
         void  Delete(TEntity entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);


    }

}
