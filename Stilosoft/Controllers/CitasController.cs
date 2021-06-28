using Microsoft.AspNetCore.Mvc;
using Stilosoft.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
    public class CitasController : Controller
    {
        private readonly ICitaService _citaService;

        public CitasController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _citaService.ObtenerListaCitas());
        }
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
    }
}
