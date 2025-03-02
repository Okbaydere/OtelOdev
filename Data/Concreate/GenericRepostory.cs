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

        public void Insert(T entity)
        {
            var sonuc = context.Entry(entity);
            sonuc.State = EntityState.Added;
            context.SaveChanges();
        }

        public List<T> liste()
        {
            return _obj.ToList();
        }

        public List<T> liste(Expression<Func<T, bool>> filter)
        {
            return _obj.Where(filter).ToList();
        }

        public void Update(T entity)
        {
            var sonuc = context.Entry(entity);
            sonuc.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
