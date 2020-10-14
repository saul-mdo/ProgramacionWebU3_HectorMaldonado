using Actividad1_FruitStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actividad1_FruitStore.Repositories
{
    public class Repository<T> where T:class
    {
        public fruteriashopContext Context { get; set; }

        public Repository(fruteriashopContext context)
        {
            Context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public T Get(object id)
        {
            return Context.Find<T>(id);
        }

        public void Insert(T entidad)
        {
            Context.Add<T>(entidad);
            Context.SaveChanges();
        }

        public void Delete(T entidad)
        {
            Context.Remove<T>(entidad);
            Context.SaveChanges();
        }

        public void Update(T entidad)
        {
            Context.Update<T>(entidad);
            Context.SaveChanges();
        }
    }
}
