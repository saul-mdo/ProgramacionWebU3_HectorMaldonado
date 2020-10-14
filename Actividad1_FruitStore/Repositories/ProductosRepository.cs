using Actividad1_FruitStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Actividad1_FruitStore.Repositories
{
    public class ProductosRepository : Repository<Productos>
    {
        public ProductosRepository(fruteriashopContext context) : base(context) { }

        public IEnumerable<Productos> GetProductosByCategoria(string nombre)
        {
            return Context.Productos.Where(x => x.IdCategoriaNavigation.Nombre == nombre);
        }

        public Productos GetProductoByCategoriaNombre(string categoria, string nombre)
        {
            return Context.Productos.Include(x=>x.IdCategoriaNavigation).FirstOrDefault(x => x.IdCategoriaNavigation.Nombre == categoria && x.Nombre == nombre);
        }
    }
}
