using Microsoft.AspNetCore.Mvc;
using Stilosoft.Business.Abstract;
using System;
using Stilosoft.ViewModels.AbonoCompra;
using Stilosoft.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stilosoft.Model.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Stilosoft.Controllers
{
    [Authorize]
    public class AbonoCompraController : Controller
    {
        private readonly IAbonoCompraService _abonoCompraService;
        private readonly IComprasService _comprasService;
        private readonly IDetalleCompraService _detalleCompraService;
        public AbonoCompraController(IAbonoCompraService abonoCompra, IComprasService comprasService, IDetalleCompraService detalleCompraService)
        {
            _abonoCompraService = abonoCompra;
            _comprasService = comprasService;
            _detalleCompraService = detalleCompraService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int Id)
        {
            ViewBag.IdCompra = Id;
            return View(await _abonoCompraService.ObtenerListaAbonoPorId(Id));       
        }
        /*[HttpGet]
        public async Task<IActionResult> Index2(int Id)
        {
            ViewBag.IdCompra = Id;
            return View(await _abonoCompraService.ObtenerListaAbonoCompra());
        }*/
        [HttpGet]
        public IActionResult AgregarAbonoCompra(int Id )
        {
            ViewBag.IdCompra = Id;           
            return View(new AbonoCompraViewModels());
        }
        [HttpPost]
        public async Task<IActionResult> AgregarAbonoCompra(int Id, AbonoCompraViewModels abonoCompraViewModels)
        {
            if (ModelState.IsValid)
            {                
                AbonoCompra abonoCompra = new()
                {                    
                    CantAbono = abonoCompraViewModels.CantAbono,
                    FechaPago = abonoCompraViewModels.FechaPago,
                    PrecioTotal = _abonoCompraService.ObtenerAbonoPorId(Id),
                    CompraId = Id,
                    Cuotas = _abonoCompraService.ObtenerCuotasPorId(Id),
                    CuotasPagadas = _abonoCompraService.ObtenerCuotasPorId(Id),
                    MontoAbonado = _abonoCompraService.ObtenerMontoAbonadoPorId(Id)
                };

                DetalleCompra detalleCompra = await _detalleCompraService.ObtenerDetalleCompraId(Id);
                if (abonoCompra.PrecioTotal == 0)
                {
                    abonoCompra.PrecioTotal = detalleCompra.Total - abonoCompra.CantAbono;
                }
                else if (abonoCompra.PrecioTotal > 0)
                {
                    abonoCompra.PrecioTotal = abonoCompra.PrecioTotal - abonoCompra.CantAbono;
                }

                if (abonoCompra != null)
                {
                    abonoCompra.Cuotas -= 1;
                }
                if (abonoCompra.CantAbono < 0)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "La cantidad debe ser mayor a 0";
                    return RedirectToAction("index", "Compras");
                }
                try
                {
                    // await _comprasService.RegistrarCompra(compra);
                    await _abonoCompraService.GuardarAbonoCompra(abonoCompra);
                    TempData["Accion"] = "Agregar";
                    TempData["Mensaje"] = "Agregar abono exitosamente";
                    return RedirectToAction("index","Compras");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index), new { CompraId = Id });
        }
        // Eliminar
        [HttpPost]
        public async Task<IActionResult> EliminarAbono(int? Id)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     if (Id != null)
                     {
                         await _abonoCompraService.EliminarAbonoCompra(Id.Value);
                         TempData["Accion"] = "Eliminar";
                         TempData["Mensaje"] = "Abono eliminado correctamente";
                         return RedirectToAction("index", "Compras");
                    }

                     return NotFound();
                 }
                 catch (Exception)
                 {

                     throw;
                 }
             }
             else
             {
                 return NotFound();
             }
         }
    }
}