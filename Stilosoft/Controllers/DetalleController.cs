using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Stilosoft.Business.Abstract;
using Stilosoft.Model.Entities;
using Stilosoft.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Stilosoft.ViewModels.Compras;

namespace Stilosoft.Controllers
{
    public class DetalleController : Controller
    {
        private readonly IComprasService _comprasService;
        private readonly IProveedorService _proveedorService;
        private readonly IInsumoService _insumoService;
        private readonly IProductoService _productoService;
        private readonly IDetalleCompraService _detalleCompraService;

        public DetalleController(IComprasService comprasService, IProveedorService proveedorService, IProductoService productoService, IInsumoService insumoService, IDetalleCompraService detalleCompraService)
        {
            _comprasService = comprasService;
            _proveedorService = proveedorService;
            _insumoService = insumoService;
            _productoService = productoService;
            _detalleCompraService = detalleCompraService;
        }
        [HttpGet]
        public async Task<IActionResult> DetalleIndex(int Id)
        {
            ViewBag.IdCompra = Id;
            return View(await _detalleCompraService.ObtenerListaDetallePorId(Id));
        }
        [HttpGet]
        /*public IActionResult CrearDetalle(int Id)
        {
            ViewBag.IdCompra = Id;
            return View(new CompraDetalleViewModel());
            
        }*/


        // DETALLE
        [HttpGet]
        public async Task<IActionResult> CrearDetalle(int Id)
        {
            ViewBag.ListarProducto = new SelectList(await _productoService.ObtenerListaProductos(), "ProductoId", "Nombre");
            ViewBag.IdCompra = Id;
            return View(new CompraDetalleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CrearDetalle(int Id, CompraDetalleViewModel compraDetalleViewModel)
        {
            if (ModelState.IsValid)
            {
                DetalleCompra detalleCompra = new()
                {
                    CompraId = Id,
                    ProductoId = compraDetalleViewModel.ProductoId,
                    Cantidad = compraDetalleViewModel.Cantidad,
                    CantInsumo = compraDetalleViewModel.CantInsumo,
                    CantProducto = compraDetalleViewModel.CantProducto,
                    Costo = compraDetalleViewModel.Costo,
                    Medida = compraDetalleViewModel.Medida,
                    SubTotal = compraDetalleViewModel.SubTotal,
                    Iva = compraDetalleViewModel.Iva,
                    Total = compraDetalleViewModel.Total


                };

                try
                {
                    var ProductoExiste = await _detalleCompraService.ProductoExiste(detalleCompra.ProductoId);

                    if (ProductoExiste != null)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "El producto ya se encuentra registrado";
                        return View(detalleCompra);
                    }
                    await _detalleCompraService.RegistrarDetalleCompra(detalleCompra);
                    TempData["Accion"] = "Crear";
                    TempData["Mensaje"] = "Producto añadido con éxito";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Hubo un error al añadir el procuto";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Ingresaste un valor inválido";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditarDetalleCompra(int Id)
        {
            DetalleCompra detalleCompra = await _detalleCompraService.ObtenerDetalleCompraId(Id);
            CompraDetalleViewModel compraDetalleViewModel = new()
            { 
                DetalleCompraId = detalleCompra.DetalleCompraId,
                CompraId = detalleCompra.CompraId,
                ProductoId = detalleCompra.ProductoId,
                InsumoId = detalleCompra.InsumoId,
                Cantidad = detalleCompra.Cantidad,
                CantInsumo = detalleCompra.CantInsumo,
                CantProducto = detalleCompra.CantProducto,
                Costo = detalleCompra.Costo,
                Medida = detalleCompra.Medida,
                SubTotal = detalleCompra.SubTotal,
                Iva = detalleCompra.Iva,
                Total = detalleCompra.Total
            };

            return View(compraDetalleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditarDetalleCompra(CompraDetalleViewModel compraDetalleViewModel)
        {
            if (ModelState.IsValid)
            {
                DetalleCompra detalleCompra = new()
                {
                    DetalleCompraId = compraDetalleViewModel.DetalleCompraId,
                    CompraId = compraDetalleViewModel.CompraId,
                    ProductoId = compraDetalleViewModel.ProductoId,
                    InsumoId = compraDetalleViewModel.InsumoId,
                    Cantidad = compraDetalleViewModel.Cantidad,
                    CantInsumo = compraDetalleViewModel.CantInsumo,
                    CantProducto = compraDetalleViewModel.CantProducto,
                    Costo = compraDetalleViewModel.Costo,
                    Medida = compraDetalleViewModel.Medida,
                    SubTotal = compraDetalleViewModel.SubTotal,
                    Iva = compraDetalleViewModel.Iva,
                    Total = compraDetalleViewModel.Total
                };

                try
                {
                   
                    await _detalleCompraService.EditarDetalle(detalleCompra);
                    TempData["Accion"] = "Editar";
                    TempData["Mensaje"] = "Producto editado correctamente";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Ingresaste un valor inválido";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Ingresaste un valor inválido";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarDetalleCompra(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DetalleCompra detalleCompra = await _detalleCompraService.ObtenerDetalleCompraId(Id);
                    await _detalleCompraService.EliminarDetalleCompra(Id);
                    TempData["Accion"] = "Eliminar";
                    TempData["Mensaje"] = "Producto eliminado correctamente";
                    return RedirectToAction("DetalleIndex");
                }
                catch (Exception)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Ocurrió un error";
                    return RedirectToAction("DetalleIndex");
                }
            }
            else
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Ocurrió un error";
                return RedirectToAction("DetalleIndex");
            }

        }

    }
}