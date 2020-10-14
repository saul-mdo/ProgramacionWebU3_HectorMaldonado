using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Actividad1_FruitStore.Models;
using Actividad1_FruitStore.Models.ViewModels;
using Actividad1_FruitStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Actividad1_FruitStore.Controllers
{
    public class HomeController : Controller
    {
        [Route("Home/Index")]
        [Route("Home")]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("{id}")]
        public IActionResult Categoria(string id)
        {
            using (fruteriashopContext context = new fruteriashopContext())
            {
                ProductosRepository repos = new ProductosRepository(context);

                CategoriaViewModel vm = new CategoriaViewModel();

                vm.Nombre = id;
                vm.Productos = repos.GetProductosByCategoria(id).ToList();

                return View(vm);
            }
        }

        [Route("detalles/{categoria}/{id}")]
        public IActionResult Ver(string categoria, string id)
        {
            using (fruteriashopContext context = new fruteriashopContext())
            {
                ProductosRepository repos = new ProductosRepository(context);

                Productos p = repos.GetProductoByCategoriaNombre(categoria, id.Replace("-", " "));

                return View(p);
            }
        }
    }
}
