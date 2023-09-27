namespace CustomerManagement.Infrastructure.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
         IQueryable<TEntity> Table();
         IQueryable<TEntity> TableNoTracking();
         void  Update(TEntity entity);
         void  Delete(TEntity entity);
      
    }

}
