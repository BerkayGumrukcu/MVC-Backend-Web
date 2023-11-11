using Entities.Context;
using Entities.Core;
using Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Base
{
    public class BaseService<T> : IService<T> where T : CoreEntity
    {
        private ProjectContext _context;

        public BaseService(ProjectContext context)
        {
            _context = context;
        }

        public bool Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return true;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public List<T> GetByDefault(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Where(exp).ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public bool Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            return true;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public bool Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}