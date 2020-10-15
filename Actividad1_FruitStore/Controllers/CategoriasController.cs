using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Actividad1_FruitStore.Models;
using Actividad1_FruitStore.Repositories;
using Microsoft.CodeAnalysis.Operations;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;

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
            // ESTO SE TIENE QUE HACER EN CASO DE QUE SE HAGA UNA ELIMINACION LOGICA
            //return View(repos.GetAll().Where(x=>x.Eliminado==false).OrderBy(x => x.Nombre));


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
            using (fruteriashopContext context = new fruteriashopContext())
            {
                CategoriasRepository repos = new CategoriasRepository(context);

                var categoria = repos.Get(id);
                if (categoria == null)
                {
                    return RedirectToAction("Index");
                }

                return View(categoria);
            }
        }

        [HttpPost]
        public IActionResult Editar(Categorias c)
        {
            try
            {
                using (fruteriashopContext context = new fruteriashopContext())
                {
                    CategoriasRepository repos = new CategoriasRepository(context);
                    var original = repos.Get(c.Id);
                    original.Nombre = c.Nombre;
                    repos.Update(original);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(c);
            }
        }


        public IActionResult Eliminar(int id)
        {
            using (fruteriashopContext context = new fruteriashopContext())
            {
                CategoriasRepository repos = new CategoriasRepository(context);
                var categoria = repos.Get(id);

                if (categoria == null)
                {
                    return RedirectToAction("Index");
                }
                else
                    return View(categoria);

            }
        }

        [HttpPost]
        public IActionResult Eliminar(Categorias c)
        {
            try
            {
                // ELIMINACIÓN FISICA (SE BORRA COMPLETAMENTE DE LA BASE DE DATOS)
                using (fruteriashopContext context = new fruteriashopContext())
                {
                    CategoriasRepository repos = new CategoriasRepository(context);
                    var categoria = repos.Get(c.Id);
                    repos.Delete(categoria);
                    return RedirectToAction("Index");
                }

                // ELIMINACION LOGICA (SE QUEDA EN LA BASE DE DATOS PERO CON UN CAMPO PARA DECIR QUE ESTÁ ELIMINADA)
                //using (fruteriashopContext context = new fruteriashopContext())
                //{
                //    CategoriasRepository repos = new CategoriasRepository(context);
                //    var categoria = repos.Get(c.Id);
                //    categoria.Eliminado = true;
                //    repos.Update(categoria);
                //    return RedirectToAction("Index");
                //}

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(c);
            }

        }
    }
}
