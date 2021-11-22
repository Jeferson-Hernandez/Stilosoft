using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
    public class SolicitudServicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
