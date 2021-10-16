using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
    public class VistasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VistaProducto()
        {
            return View();
        }

        public IActionResult VistaServicio()
        {
            return View();
        }

        public IActionResult VistaInsumo()
        {
            return View();
        }

        public IActionResult VistaPrincipal()
        {
            return View();
        }

        public IActionResult VistaCategoria()
        {
            return View();
        }

        public IActionResult VistaAgregarCategoria()
        {
            return View();
        }
    }
}
