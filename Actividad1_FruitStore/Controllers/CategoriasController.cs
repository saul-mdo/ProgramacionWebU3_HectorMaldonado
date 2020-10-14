using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Actividad1_FruitStore.Models;
using Actividad1_FruitStore.Repositories;
using Microsoft.CodeAnalysis.Operations;
using System.Security.Cryptography.X509Certificates;

namespace Actividad1_FruitStore.Controllers
{
    public class CategoriasController : Controller
    {
        [Route("Categorias")]
        public IActionResult Index()
        {
            fruteriashopContext context = new fruteriashopContext();
            Repository<Categorias> repos = new Repository<Categorias>(context);
            return View(repos.GetAll().OrderBy(x => x.Nombre));
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Categorias c)
        {
            try
            {
                fruteriashopContext context = new fruteriashopContext();
                CategoriasRepository repos = new CategoriasRepository(context);
                repos.Insert(c);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(c);
            }

        }

        public IActionResult Editar(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Editar(Categorias c)
        {
            return View();
        }


        public IActionResult Eliminar(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Eliminar(Categorias c)
        {
            return View();
        }
    }
}
