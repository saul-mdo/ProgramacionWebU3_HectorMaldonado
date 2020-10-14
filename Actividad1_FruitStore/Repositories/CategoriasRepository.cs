using Actividad1_FruitStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Actividad1_FruitStore.Repositories
{
    public class CategoriasRepository:Repository<Categorias>
    {
        public CategoriasRepository(fruteriashopContext context):base(context)
        {

        }

        public override bool Validate(Categorias entidad)
        {
            if (string.IsNullOrWhiteSpace(entidad.Nombre))
            {
                throw new Exception("No escribió el nombre de la categoría");
            }
            if (Context.Categorias.Any(x => x.Nombre == entidad.Nombre))
            {
                throw new Exception("Esta categoría ya está registrada");
            }
            return true; 
        }

    }
}
