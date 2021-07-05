﻿using Microsoft.AspNetCore.Hosting;
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
    public class ComprasController : Controller
    {
        private readonly IComprasService _comprasService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IProveedorService _proveedorService;
        private readonly IInsumoService _insumoService;
        private readonly IProductoService _productoService;

        public ComprasController(IComprasService comprasService, IWebHostEnvironment hostEnvironment, IProveedorService proveedorService, IProductoService productoService, IInsumoService insumoService)
        {
            _comprasService = comprasService;
            _hostEnvironment = hostEnvironment;
            _proveedorService = proveedorService;
            _insumoService = insumoService;
            _productoService = productoService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _comprasService.ObtenerListaCompras());
        }
        public async Task<IActionResult> DetalleIndex()
        {
            
            return View(await _comprasService.ObtenerDetalleCompras());
            
        }

        [HttpGet]
        public async Task<IActionResult> CrearCompra()
        {
            ViewBag.ListarProveedor = new SelectList(await _proveedorService.ObtenerListaProveedores(), "ProveedorId", "Nombre");
            return View(new ComprasViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> CrearCompra(ComprasViewModel comprasViewModel)
        {
            //preguntamos si el modelo es válido o no (comprueba validaciones)
            if (ModelState.IsValid)
            {
                // se crea un objeto de tipo empleado y se le asignan las propiedades que vienen de empleadoViewModel
                Compra compra = new()
                {
                    ProveedorId = comprasViewModel.ProveedorId,
                    FechaFactura = comprasViewModel.FechaFactura,
                    NoFactura = comprasViewModel.NoFactura,
                    FormaPago = comprasViewModel.FormaPago,
                    FechaInicioPago = comprasViewModel.FechaInicioPago,
                    FechaRegistro = comprasViewModel.FechaRegistro,
                    Periodicidad = comprasViewModel.Periodicidad,
                    Cuotas = comprasViewModel.Cuotas
                };
                var CompraExiste = await _comprasService.NoFacturaExiste(compra.NoFactura);

                if (CompraExiste != null)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "La compra ya se encuentra registrada";
                    return View(comprasViewModel);
                }
                if (compra.Cuotas <= 0)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Las cuotas deben ser mayor a 0";
                    return RedirectToAction("Index");
                }
                string path = null;
                string wwwRootPath = null;

                // si se utiliza una imagen entonces
                if (comprasViewModel.Imagen != null)
                {
                    //obtenemos la ruta raiz de nuestro proyecto
                    wwwRootPath = _hostEnvironment.WebRootPath;
                    //obtenemos el nombre de la imagen
                    string NombreImagen = Path.GetFileNameWithoutExtension(comprasViewModel.Imagen.FileName);
                    //obtenemos la extensión de la imagen .jpg - .png etc
                    string Extension = Path.GetExtension(comprasViewModel.Imagen.FileName);
                    //concatenamos el nombre de la imagen con el año-minuto-segundos-fraciones de segundo + la extensión
                    compra.RutaImagen = NombreImagen + DateTime.Now.ToString("yymmssfff") + Extension;
                    //Obetenemos la ruta en donde vamos a guardar la imagen
                    path = Path.Combine(wwwRootPath + "/imagenes/" + compra.RutaImagen);
                }
                try
                {
                    // si se va a guardar una imagen
                    if (path != null)
                    {
                        //copiamos la imagen a la ruta especifica
                        using var FileStream = new FileStream(path, FileMode.Create);
                        await comprasViewModel.Imagen.CopyToAsync(FileStream);
                    }

                    await _comprasService.RegistrarCompra(compra);
                    TempData["Accion"] = "RegistrarCompra";
                    TempData["Mensaje"] = "Compra guardada con éxito";
                    return RedirectToAction("CrearDetalle");
                }
                catch (Exception)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Error realizando la operación";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(comprasViewModel);
            }
        }
       
        [HttpPost]
        public async Task<IActionResult> EliminarCompra(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Compra compra = await _comprasService.ObtenerCompraPorId(Id);

                    if (compra.RutaImagen != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        FileInfo file = new FileInfo(wwwRootPath + "/imagenes/" + compra.RutaImagen);
                        file.Delete();
                    }

                    await _comprasService.EliminarCompra(Id);
                    TempData["Accion"] = "EliminarCompra";
                    TempData["Mensaje"] = "Compra eliminada correctamente";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Ocurrió un error";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Ocurrió un error";
                return RedirectToAction("Index");
            }

        }

        // DETALLE
        [HttpGet]
        public async Task<IActionResult> CrearDetalleCompra()
        {
            ViewBag.ListarProducto = new SelectList(await _productoService.ObtenerListaProductos(),"ProductoId", "Nombre");
            return View(new CompraDetalleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CrearDetalleCompra(CompraDetalleViewModel compraDetalleViewModel)
        {
            if (ModelState.IsValid)
            {
                DetalleCompra detalleCompra = new()
                {
                   
                    
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
                    var ProductoExiste = await _comprasService.ProductoExiste(detalleCompra.ProductoId);

                    if (ProductoExiste != null)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "El producto ya se encuentra registrado";
                        return View(detalleCompra);
                    }
                    await _comprasService.RegistrarDetalleCompra(detalleCompra);
                    TempData["Accion"] = "Crear";
                    TempData["Mensaje"] = "Producto añadido con éxito";
                    return RedirectToAction("DetalleIndex");
                }
                catch (Exception)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Hubo un error al añadir el procuto";
                    return RedirectToAction("DetalleIndex");
                }
            }
            else
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Ingresaste un valor inválido";
                return RedirectToAction("DetalleIndex");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditarDetalleCompra(int Id)
        {
            DetalleCompra detalleCompra = await _comprasService.ObtenerDetalleCompraPorId(Id);
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
                   
                    await _comprasService.EditarDetalleCompra(detalleCompra);
                    TempData["Accion"] = "Editar";
                    TempData["Mensaje"] = "Producto editado correctamente";
                    return RedirectToAction("DetalleIndex");
                }
                catch (Exception)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Ingresaste un valor inválido";
                    return RedirectToAction("DetalleIndex");
                }
            }
            else
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Ingresaste un valor inválido";
                return RedirectToAction("DetalleIndex");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarDetalleCompra(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DetalleCompra detalleCompra = await _comprasService.ObtenerDetalleCompraPorId(Id);
                    await _comprasService.EliminarDetalleCompra(Id);
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