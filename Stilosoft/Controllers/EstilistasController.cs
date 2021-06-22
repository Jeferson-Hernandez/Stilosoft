using Microsoft.AspNetCore.Mvc;
using Stilosoft.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
    public class EstilistasController : Controller
    {
        private readonly IEstilistaService _estilistaService;

        public EstilistasController(IEstilistaService estilistaService)
        {
            _estilistaService = estilistaService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _estilistaService.ObtenerListaEstilistas());
        }
    }
}
