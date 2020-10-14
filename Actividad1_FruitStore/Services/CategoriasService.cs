using Actividad1_FruitStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actividad1_FruitStore.Repositories;

namespace Actividad1_FruitStore.Services
{
    public class CategoriasService
    {
        public List<Categorias> Categorias { get; set; }

        public CategoriasService()
        {
            using (fruteriashopContext context = new fruteriashopContext())
            {
                Repository<Categorias> repos = new Repository<Categorias>(context);
                Categorias = repos.GetAll().OrderBy(x=>x.Nombre).ToList();
            }
        }
    }
}
