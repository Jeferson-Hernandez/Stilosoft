﻿using Microsoft.AspNetCore.Mvc;
using Stilosoft.Business.Abstract;
using System;
using Stilosoft.ViewModels.AbonoCompra;
using Stilosoft.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stilosoft.Model.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Stilosoft.Controllers
{
    public class AbonoCompraController : Controller
    {
        private readonly IAbonoCompraService _abonoCompraService;
        private readonly IComprasService _comprasService;

        public AbonoCompraController(IAbonoCompraService abonoCompra, IComprasService comprasService)
        {
            _abonoCompraService = abonoCompra;
            _comprasService = comprasService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int Id)
        {
            ViewBag.IdCompra = Id;
            return View(await _abonoCompraService.ObtenerListaAbonoPorId(Id));
        }
        public IActionResult AgregarAbonoCompra(int Id )
        {
            ViewBag.IdCompra = Id;           
            return View(new AbonoCompraViewModels());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    CuotasPagadas = 5,
                    MontoAbonado = 10000
                };

                Compra compra = await _comprasService.ObtenerCompraPorId(Id);
                if (abonoCompra.PrecioTotal == 0)
                {
                    abonoCompra.PrecioTotal = compra.PrecioTotal - abonoCompra.CantAbono;
                }
                else if (abonoCompra.PrecioTotal > 0)
                {
                    abonoCompra.PrecioTotal = abonoCompra.PrecioTotal - abonoCompra.CantAbono;
                }

                if (abonoCompra != null)
                {
                    abonoCompra.Cuotas -= 1;
                }

                try
                {
                    // await _comprasService.RegistrarCompra(compra);
                    await _abonoCompraService.GuardarAbonoCompra(abonoCompra);
                    TempData["Accion"] = "CrearInsu";
                    TempData["Mensaje"] = "Insumo creado exitosamente";
                    return RedirectToAction(nameof(Index), new { CompraId = Id });
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
                         TempData["Accion"] = "EliminarInsu";
                         TempData["Mensaje"] = "Insumo eliminado correctamente";
                         return RedirectToAction("Index");
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