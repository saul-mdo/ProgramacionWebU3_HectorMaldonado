using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Actividad1_FruitStore.Controllers
{
    public class HomeController : Controller
    {
        // MIO
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Categoria()
        {
            return View();
        }

        public IActionResult Ver()
        {
            return View();
        }
    }
}
