using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ZooPlanet.Models;
using ZooPlanet.Models.ViewModels;
using ZooPlanet.Repositories;

namespace ZooPlanet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        animalesContext context;
        public IWebHostEnvironment Enviroment { get; set; }
        public HomeController(animalesContext ctx, IWebHostEnvironment env)
        {
            context = ctx;
            Enviroment = env;
        }

        public IActionResult Index()
        {
            EspeciesRepository repos = new EspeciesRepository(context);
            return View(repos.GetAll());
        }

        public IActionResult Agregar()
        {
            EspecieViewModel vm = new EspecieViewModel();
            ClasesRepository repos = new ClasesRepository(context);
            vm.Clases = repos.GetAll();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Agregar(EspecieViewModel evm)
        {
            try
            {
                EspeciesRepository especierepos = new EspeciesRepository(context);

                especierepos.Insert(evm.Especies);

                return RedirectToAction("Index","Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ClasesRepository clasesrepos = new ClasesRepository(context);
                evm.Clases = clasesrepos.GetAll();
                return View(evm);
            }
        }

        public IActionResult Editar(int id)
        {
            EspeciesRepository especierepos = new EspeciesRepository(context);
            ClasesRepository clasesrepos = new ClasesRepository(context);
            EspecieViewModel evm = new EspecieViewModel();
            evm.Especies = especierepos.GetById(id);
            evm.Clases = clasesrepos.GetAll();
            if (evm.Especies == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            return View(evm);
        }

        [HttpPost]
        public IActionResult Editar(EspecieViewModel evm)
        {
            try
            {
                EspeciesRepository especierepos = new EspeciesRepository(context);
                ClasesRepository clasesrepos = new ClasesRepository(context);
                var original = especierepos.GetById(evm.Especies.Id);
                evm.Clases = clasesrepos.GetAll();
                if (evm.Especies != null)
                {
                    original.Especie = evm.Especies.Especie;
                    original.Habitat = evm.Especies.Habitat;
                    original.Peso = evm.Especies.Peso;
                    original.Tamaño = evm.Especies.Tamaño;
                    original.Observaciones = evm.Especies.Observaciones;
                    especierepos.Update(original);
                    return RedirectToAction("Index", "Home");
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ClasesRepository clasesrepos = new ClasesRepository(context);
                evm.Clases = clasesrepos.GetAll();
                return View(evm);
            }
        }

        public IActionResult Eliminar(int id)
        {
            EspeciesRepository especierepos = new EspeciesRepository(context);
            var especie = especierepos.GetById(id);
            if (especie != null)
            {
                return View(especie);
            }
            else
                return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Eliminar(Especies esp)
        {
            EspeciesRepository especierepos = new EspeciesRepository(context);
            var original = especierepos.GetById(esp.Id);
            if (original != null)
            {
                especierepos.Delete(original);
                return RedirectToAction("Index", "Home");
            }
            else
            {

                ModelState.AddModelError("", "El producto no existe o ya fué eliminado");
                return View();
            }
        }

        public IActionResult Imagen(int id)
        {
            EspeciesRepository especierepos = new EspeciesRepository(context);
            EspecieViewModel evm = new EspecieViewModel();
            evm.Especies = especierepos.GetById(id);
            if (System.IO.File.Exists(Enviroment.WebRootPath + $"/especies/{evm.Especies.Id}.jpg"))
            {
                evm.Imagen = evm.Especies.Id + ".jpg";
            }
            else
            {
                evm.Imagen = "nophoto.jpg";
            }
            return View(evm);
        }
        [HttpPost]
        public IActionResult Imagen(EspecieViewModel evm)
        {
            try
            {
                if (evm.Archivo == null)
                {
                    ModelState.AddModelError("", "Debe seleccionar la imagen del producto.");
                    return View(evm);
                }
                else
                {
                    if (evm.Archivo.ContentType != "image/jpeg" || evm.Archivo.Length > 1024 * 1024 * 2)
                    {
                        ModelState.AddModelError("", "Debe seleccionar un archivo jpg de menos de 2MB.");
                        return View(evm);
                    }
                }
                if (evm.Archivo != null)
                {
                    FileStream fs = new FileStream(Enviroment.WebRootPath + "/especies/" + evm.Especies.Id + ".jpg", FileMode.Create);
                    evm.Archivo.CopyTo(fs);
                    fs.Close();
                }

                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(evm);
            }
        }
    }
}
