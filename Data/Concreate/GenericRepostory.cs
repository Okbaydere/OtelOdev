using Data.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concreate
{
    public class GenericRepostory<T> : IGenericRepostory<T> where T : class
    {
        Context context = new Context();
        DbSet<T> _obj;

        public GenericRepostory()
        {
            _obj = context.Set<T>();
        }

        public T GetWithIncludes(Expression<Func<T, bool>> filter, params string[] includes)
        {
            var query = _obj.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query.SingleOrDefault(filter);
        }

        public List<T> ListWithIncludes(Expression<Func<T, bool>> filter = null, params string[] includes)
        {
            var query = _obj.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return filter != null ? query.Where(filter).ToList() : query.ToList();
        }

        public void Delete(T entity)
        {
            var sonuc = context.Entry(entity);
            sonuc.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _obj.SingleOrDefault(filter);
        }

        public List<T> liste(Expression<Func<T, bool>> filter = null)
        {
            return filter != null ? _obj.Where(filter).ToList() : _obj.ToList();
        }

        public void Insert(T entity)
        {
            var sonuc = context.Entry(entity);
            sonuc.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            var sonuc = context.Entry(entity);
            sonuc.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
