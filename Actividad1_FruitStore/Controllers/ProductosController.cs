using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Actividad1_FruitStore.Models;
using Actividad1_FruitStore.Models.ViewModels;
using Actividad1_FruitStore.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Actividad1_FruitStore.Controllers
{
    public class ProductosController : Controller
    {
        public IWebHostEnvironment Enviroment { get; set; }
        public ProductosController(IWebHostEnvironment env)
        {
            Enviroment = env;
        }

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

            if(vm.Archivo.ContentType!="image/jpeg" || vm.Archivo.Length > 1024 * 1024 * 2)
            {
                ModelState.AddModelError("", "Debe seleccionar un archivo jpg de menos de 2MB.");
                CategoriasRepository repos = new CategoriasRepository(context);
                vm.Categorias = repos.GetAll().OrderBy(x => x.Nombre);
                return View(vm);
            }


            try
            {
                ProductosRepository repos = new ProductosRepository(context);
                repos.Insert(vm.Producto);

                FileStream fs = new FileStream(Enviroment.WebRootPath+"/img_frutas/"+vm.Producto.Id+".jpg",FileMode.Create);
                vm.Archivo.CopyTo(fs);
                fs.Close();


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
