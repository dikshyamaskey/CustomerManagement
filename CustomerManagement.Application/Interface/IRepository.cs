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
        Task AddRangeAsync(IEnumerable<TEntity> entity);
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken);


    }

}
