using Microsoft.AspNetCore.Mvc;
using Stilosoft.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
    public class AbonoCompraController : Controller
    {
        private readonly IAbonoCompraService _abonoCompraService;
         public AbonoCompraController(IAbonoCompraService abonoCompra)
        {
            _abonoCompraService = abonoCompra;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _abonoCompraService.ObtenerListaAbonoCompra());

        }
    }
}
