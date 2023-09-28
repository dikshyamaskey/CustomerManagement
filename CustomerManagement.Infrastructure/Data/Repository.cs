using CustomerManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Infrastructure.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<TEntity> _entity;
        public Repository(ApplicationDbContext dbContext)
        {

            _dbContext = dbContext;
            _entity = _dbContext.Set<TEntity>();

        }
  
        public IQueryable<TEntity> Table()
        {
            return _entity.AsQueryable();
        }

        public IQueryable<TEntity> TableNoTracking()
        {
            return _entity.AsNoTracking();
        }

        public  TEntity Insert(TEntity entity)
        {
             _dbContext.Set<TEntity>().Add(entity);
            return entity;
        }

       
        public  void Update(TEntity entity)
        {
            _entity.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        public  void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _entity.Attach(entity);
            }
            _entity.Remove(entity);
        }
        public async Task<int>  SaveChangesAsync(CancellationToken cancellationToken)
        {
            return  await _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
