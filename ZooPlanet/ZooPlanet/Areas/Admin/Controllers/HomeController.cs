using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public HomeController(animalesContext ctx)
        {
            context = ctx;
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

                return RedirectToAction("Index","Home",new { area="Admin"});
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
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else
            return View(evm);
        }

        [HttpPost]
        public IActionResult Editar(EspecieViewModel evm)
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
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else
                return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        public IActionResult Eliminar(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Eliminar(EspecieViewModel evm)
        {
            return View();
        }

        public IActionResult Imagen(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Imagen(Especies esp)
        {
            return View();
        }
    }
}
