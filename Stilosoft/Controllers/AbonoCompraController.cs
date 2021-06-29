using Microsoft.AspNetCore.Mvc;
using Stilosoft.Business.Abstract;
using System;
using Stilosoft.ViewModels.AbonoCompra;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stilosoft.Model.Entities;

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
        public IActionResult AgregarAbonoCompra()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarAbonoCompra(AbonoCompraViewModels abonoCompraViewModels)
        {

            if (ModelState.IsValid)
            {
                AbonoCompra abonoCompra = new()
                {
                    CantAbono = abonoCompraViewModels.CantAbono,
                    FechaPago = abonoCompraViewModels.FechaPago                 
                };

                try
                {
                    
                    await _abonoCompraService.GuardarAbonoCompra(abonoCompra);
                    TempData["Accion"] = "CrearInsu";
                    TempData["Mensaje"] = "Insumo creado exitosamente";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
