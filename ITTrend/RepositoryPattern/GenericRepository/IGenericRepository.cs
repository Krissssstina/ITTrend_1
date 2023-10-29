using System.Linq.Expressions;

namespace ITTrend.RepositoryPattern.GenericRepository
{

    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public TEntity Insert(TEntity entity);
        public void Insert(params TEntity[] entities);
        public void Insert(IEnumerable<TEntity> entities);
        public void Remove(int? id);
        public void Remove(TEntity entity);
        public void Remove(params TEntity[] entities);
        public void Remove(IEnumerable<TEntity> entities);
        public void Update(TEntity entity);
        public void Update(IEnumerable<TEntity> entities);
        public void Update(params TEntity[] entities);
        public void InsertOrUpdate(TEntity entity);
        public void InsertOrUpdate(params TEntity[] entities);
        public IEnumerable<TEntity> GetEntities();
        public IEnumerable<TEntity> GetEntities(params Expression<Func<TEntity, object>>[] includes);
    }
}

