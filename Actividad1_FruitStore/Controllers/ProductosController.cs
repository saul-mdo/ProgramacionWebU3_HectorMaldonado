using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actividad1_FruitStore.Models;
using Actividad1_FruitStore.Models.ViewModels;
using Actividad1_FruitStore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Actividad1_FruitStore.Controllers
{
    public class ProductosController : Controller
    {
        public IActionResult Index()
        {
            ProductosIndexViewModel vm = new ProductosIndexViewModel();
            fruteriashopContext context = new fruteriashopContext();
            CategoriasRepository categoriarepos = new CategoriasRepository(context);
            ProductosRepository productosrepos = new ProductosRepository(context);

            int? id = null;

            vm.Categorias = categoriarepos.GetAll().OrderBy(x=>x.Nombre);
            vm.Productos = productosrepos.GetProductosByCategoria(id);


            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(ProductosIndexViewModel vm)
        {
            fruteriashopContext context = new fruteriashopContext();
            CategoriasRepository categoriarepos = new CategoriasRepository(context);
            ProductosRepository productosrepos = new ProductosRepository(context);


            vm.Categorias = categoriarepos.GetAll().OrderBy(x => x.Nombre);
            vm.Productos = productosrepos.GetProductosByCategoria(vm.IdCategoria);

            return View(vm);
        }

        public IActionResult Agregar()
        {
            ProductosViewModel vm = new ProductosViewModel();
            fruteriashopContext context = new fruteriashopContext();
            CategoriasRepository repos = new CategoriasRepository(context);
            vm.Categorias = repos.GetAll().OrderBy(x=>x.Nombre);
            return View(vm);
        }
        [HttpPost]
        public IActionResult Agregar(ProductosViewModel vm)
        {
            fruteriashopContext context = new fruteriashopContext();
            try
            {
                ProductosRepository repos = new ProductosRepository(context);
                repos.Insert(vm.Producto);
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                CategoriasRepository repos = new CategoriasRepository(context);
                vm.Categorias = repos.GetAll().OrderBy(x => x.Nombre);
                return View(vm);
            }
        }


        public IActionResult Editar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Editar(Productos p)
        {
            return View();
        }


        public IActionResult Eliminar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Eliminar(Productos p)
        {
            return View();
        }
    }
}
