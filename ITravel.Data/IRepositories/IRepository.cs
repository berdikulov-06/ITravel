using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Data.IRepositories
{
    public interface IRepository<T> where T : class
    {
        public Task<T?> FindByIdAsync(long id);

        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);

        public void Add(T entity);
        public T Create(T entity);


        public void Delete(long id);

        public void Update(T entity);

        public IQueryable<T> GetAll();

        public IQueryable<T> Where(Expression<Func<T, bool>> expression);

        public T LastData();
    }
}
