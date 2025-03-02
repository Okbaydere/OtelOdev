using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface IGenericRepostory<T> where T : class
    {
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        List<T> liste();
        List<T> liste(Expression<Func<T, bool>> filter);
        T Get(Expression<Func<T, bool>> filter);
        T GetWithIncludes(Expression<Func<T, bool>> filter, params string[] includes);
        List<T> ListWithIncludes(Expression<Func<T, bool>> filter = null, params string[] includes);
    }
}
