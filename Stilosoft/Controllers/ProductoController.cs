using Microsoft.AspNetCore.Mvc;
using Stilosoft.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;
       
        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _productoService.ObtenerListaProductos());
        }
    }
}
