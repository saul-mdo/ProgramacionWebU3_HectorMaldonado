using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ZooPlanet.Models;
using ZooPlanet.Repositories;

namespace ZooPlanet.Controllers
{
    public class AdministradorController : Controller
    {



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Agregar()
        {
            return View();
        }



        public IActionResult Editar(int id)
        {
            return View();
        }

        public IActionResult Eliminar(int id)
        {
            return View();
        }



        public IActionResult Imagen(int id)
        {
            return View();
        }



    }
}