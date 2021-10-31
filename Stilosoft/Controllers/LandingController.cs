using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
    public class LandingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Insumos()
        {
            return View();
        }
    }
}
