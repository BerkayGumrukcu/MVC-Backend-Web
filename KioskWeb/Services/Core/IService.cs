using Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core
{
    public interface IService<T> where T : CoreEntity
    {
        public bool Add(T entity);
        public bool Update(T entity);
        public bool Remove(T entity);
        public T GetById(int id);
        public int Save();
        public List<T> GetAll();
        public List<T> GetByDefault(Expression<Func<T, bool>> exp);

    }
}
