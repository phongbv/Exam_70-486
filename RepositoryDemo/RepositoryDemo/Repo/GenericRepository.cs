using RepositoryDemo.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RepositoryDemo.Repo
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private DbSet<TEntity> dbSet;
        private DataContext dataContext;
        public GenericRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
            dbSet = dataContext.Set<TEntity>();
        }
        public IEnumerable<TEntity> GetAllRecords()
        {
            return dbSet.ToList();
        }
        public IEnumerable<TEntity> GetAllRecordsWithoutTracking()
        {
            return dbSet.AsNoTracking().ToList();
        }
        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Deleted;
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }
    }
}