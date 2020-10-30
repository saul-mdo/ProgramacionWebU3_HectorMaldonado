using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZooPlanet.Models;

namespace ZooPlanet.Repositories
{
	public class EspeciesRepository : Repository<Especies>
	{

        public EspeciesRepository(animalesContext ctx):base(ctx)
        {

        }


		public override IEnumerable<Especies> GetAll()
		{
			return base.GetAll().OrderBy(x=>x.Especie);
		}

		public IEnumerable<Especies> GetEspeciesByClase(string Id)
		{
			return Context.Especies
				.Include(x => x.IdClaseNavigation)
				.Where(x => x.IdClaseNavigation.Nombre == Id)
				.OrderBy(x => x.Especie);
		}

        public override bool Validate(Especies entidad)
        {
			if (Context.Especies.Any(x => x.Especie.ToUpper() == entidad.Especie.ToUpper() && x.Id != entidad.Id))
				throw new Exception("Ya hay una especie registrada con ese nombre.");
			if (string.IsNullOrWhiteSpace(entidad.Especie))
				throw new Exception("Escriba el nombre de la especie.");
			if(string.IsNullOrWhiteSpace(entidad.Habitat))
				throw new Exception("Escriba el Habitat de la especie.");
			if (entidad.Peso<=0 || entidad.Peso==null)
				throw new Exception("Escriba el peso de la especie.");
			if (entidad.Tamaño <= 0 || entidad.Tamaño == null)
				throw new Exception("Escriba el tamaño de la especie.");
			if (string.IsNullOrWhiteSpace(entidad.Observaciones))
				throw new Exception("Escriba las observaciones de la especie.");
			return true;
        }


    }
}
