using ITravel.Data.Contexts;
using ITravel.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected TourismDbContext _dbcontext;
        protected DbSet<T> _dbSet;
        public BaseRepository(TourismDbContext context)
        {
            _dbcontext = context;
            _dbSet = context.Set<T>();
        }
        public virtual void Add(T entity) => _dbSet.Add(entity);

        public virtual T Create(T entity)
        {
            var result = _dbSet.Add(entity);

            return result.Entity;
        }

        public virtual void Delete(long id)
        {
            var entity = _dbSet.Find(id);
            if (entity is not null)
                _dbSet.Remove(entity);
        }

        public virtual async Task<T?> FindByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> GetAll() => _dbSet;

        public T LastData()
        {
            return _dbSet.LastOrDefault();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
            => _dbSet.Where(expression);
    }
}
