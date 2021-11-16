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
using Stilosoft.Model.DAL;

namespace Stilosoft.Controllers
{
    [Authorize]
    public class AbonoCompraController : Controller
    {
        private readonly IAbonoCompraService _abonoCompraService;
        private readonly IComprasService _comprasService;
        private readonly IDetalleCompraService _detalleCompraService;
        private readonly AppDbContext _context;
        public AbonoCompraController(IAbonoCompraService abonoCompra, IComprasService comprasService, IDetalleCompraService detalleCompraService, AppDbContext context)
        {
            _abonoCompraService = abonoCompra;
            _comprasService = comprasService;
            _detalleCompraService = detalleCompraService;
            _context = context;
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
        public async Task<IActionResult> AgregarAbonoCompra(int Id)
        {
            ViewBag.IdCompra = Id;
            DetalleCompra detalleCompra = await _detalleCompraService.ObtenerDetalleCompraId(Id);
            Compra compra = await _comprasService.ObtenerCompraPorId(Id);
       
              AbonoCompraViewModels abonoCompraViewModels = new()
            {
                Cuotas = compra.Cuotas,
                ValorInicial = detalleCompra.Total,

            };
                                 
            return View(new AbonoCompraViewModels());
        }
        [HttpPost]
        public async Task<IActionResult> AgregarAbonoCompra(int Id, AbonoCompraViewModels abonoCompraViewModels )
        {
            ////Poner un mensaje de alerta, que el pago por abonos fue completado
            if (ModelState.IsValid)
            {
                DetalleCompra detalleCompra = await _detalleCompraService.ObtenerDetalleCompraId(Id);
                Compra compra = await _comprasService.ObtenerCompraPorId(Id);
                AbonoCompra abonoCompra1 = await _abonoCompraService.ObtenerAbonoCompraId(Id);
                AbonoCompra abonoCompra = new()
                {
                    CantAbono = abonoCompraViewModels.CantAbono,
                    FechaPago = abonoCompraViewModels.FechaPago,
                    Cuotas = abonoCompraViewModels.Cuotas,
                    ValorInicial = abonoCompraViewModels.ValorInicial,
                    ValorFinal = abonoCompraViewModels.ValorFinal,
                    CompraId = Id                                           
            };

                if (!AbonoCompraExists(abonoCompra.CompraId))
                {
                    abonoCompra.ValorInicial = _abonoCompraService.ObtenerTotalDetalleCompraPorId(Id);
                    abonoCompra.Cuotas = compra.Cuotas;
                }
                else
                {
                    abonoCompra.Cuotas = _abonoCompraService.ObtenerAbonoCuotaPorId(Id);
                    abonoCompra.ValorInicial = _abonoCompraService.ObtenerAbonoPorId(Id);
                }       
                if (abonoCompra.CantAbono > 0)
                {
                    abonoCompra.ValorFinal = abonoCompra.ValorInicial - abonoCompra.CantAbono;
                }
                if(abonoCompra.Cuotas > 0)
                {
                   abonoCompra.Cuotas -= 1;
                }
                try
                {            
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
                         TempData["Accion"] = "EliminarAbono";
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
        private bool AbonoCompraExists(int id)
        {
            return _context.AbonoCompra.Where(c => c.CompraId == id).Any(e => e.ValorFinal > 0);
        }        
    }
    
}
