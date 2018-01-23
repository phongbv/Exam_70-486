using RepositoryDemo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryDemo.Repo
{
    public interface IGenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        IEnumerable<TEntity> GetAllRecords();
        IEnumerable<TEntity> GetAllRecordsWithoutTracking();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
