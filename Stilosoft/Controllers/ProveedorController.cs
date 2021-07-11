using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
    [Authorize]
    public class ProveedorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
