using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface IGenericRepostory<T>
    {
        List<T> liste();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(Expression<Func<T, bool>> filter);
        List<T> liste(Expression<Func<T, bool>> filter);
    }
}
