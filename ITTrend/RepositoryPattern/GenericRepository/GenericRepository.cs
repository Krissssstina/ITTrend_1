using ITTrend.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace ITTrend.RepositoryPattern.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
    {
        private readonly DbContext _context;
        private DbSet<TEntity> _dbSet => _context.Set<TEntity>();

        protected GenericRepository(DbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
        public virtual TEntity Insert(TEntity entity) => _dbSet.Add(entity).Entity;

        public virtual void Insert(params TEntity[] entities) => _dbSet.AddRange(entities);

        public virtual void Insert(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);

        public virtual void Remove(int? id)
        {
            var typeInfo = typeof(TEntity).GetTypeInfo();
            var key = _context.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                property.SetValue(entity, id);
                _context.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                var entity = _dbSet.Find(id);
                if (entity != null)
                {
                    Remove(entity);
                }
            }
        }

        public virtual void Remove(TEntity entity) => _dbSet.Remove(entity);

        public virtual void Remove(params TEntity[] entities) => _dbSet.RemoveRange(entities);

        public virtual void Remove(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

        public virtual void Update(TEntity entity) => _dbSet.Update(entity);

        public virtual void Update(IEnumerable<TEntity> entities) => _dbSet.UpdateRange(entities);

        public virtual void Update(params TEntity[] entities) => _dbSet.UpdateRange(entities);

        public virtual void InsertOrUpdate(TEntity entity)
        {
            if (_dbSet.Any(_ => _.Id == entity.Id)) Update(entity);
            else Insert(entity);
        }

        public virtual void InsertOrUpdate(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                if (_dbSet.Any(_ => _.Id == entity.Id)) Update(entity);
                else Insert(entity);
            }
        }

        public virtual IEnumerable<TEntity> GetEntities() => _dbSet.ToList();

        public IEnumerable<TEntity> GetEntities(params Expression<Func<TEntity, object>>[] includes)
            => includes.Aggregate(_dbSet.Where(q => true), (current, includeProperty) => current.Include(includeProperty));


        protected TEntity GetEntity(int? id) => _context.Find<TEntity>(id);

        protected TEntity GetEntity(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes) =>
            includes.Aggregate(_dbSet.Where(predicate), (current, includeProperty) => current.Include(includeProperty))
                .FirstOrDefault();
    }
}
