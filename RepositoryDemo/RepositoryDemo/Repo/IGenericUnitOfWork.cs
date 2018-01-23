using RepositoryDemo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryDemo.Repo
{
    public interface IGenericUnitOfWork<TEntity> where TEntity : class, IBaseEntity
    {
        IGenericRepository<TEntity> GetRepository();
        void SaveChanges();
    }

    public class GenericUnitOfWork<TEntity> : IGenericUnitOfWork<TEntity> where TEntity : class, IBaseEntity
    {
        private DataContext dbContext = new DataContext();
        public IGenericRepository<TEntity> GetRepository()
        {
            return new GenericRepository<TEntity>(dbContext);
        }
        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}