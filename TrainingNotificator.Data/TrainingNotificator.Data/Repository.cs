using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrainingNotificator.Core.Interfaces;

namespace TrainingNotificator.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly UsersDbContext context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(UsersDbContext context)
        {
            this.context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<bool> Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> AddRange(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return true;
        }

        public virtual bool Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            this.context.Entry(entity).State = EntityState.Deleted;
            return true;
        }

        public async Task<bool> DeleteById<TId>(TId id)
        {
            var entity = await _dbSet.FindAsync(id);
            return Delete(entity);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetById<TId>(TId id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<int> GetItemsCount(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await _dbSet.Where(predicate).CountAsync();
            }
            return await _dbSet.CountAsync();
        }

        public virtual bool Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate = null,
                                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                return orderBy(query);
            }
            return query;
        }
    }
}