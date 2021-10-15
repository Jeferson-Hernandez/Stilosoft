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
using Microsoft.AspNetCore.Authorization;

namespace Stilosoft.Controllers
{
    [Authorize]
    public class ComprasController : Controller
    {
        private readonly IComprasService _comprasService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IProveedorService _proveedorService;
        private readonly IInsumoService _insumoService;
        private readonly IProductoService _productoService;
        private readonly IDetalleCompraService _detalleCompraService;

        public ComprasController(IComprasService comprasService, IWebHostEnvironment hostEnvironment, IProveedorService proveedorService, IProductoService productoService, IInsumoService insumoService, IDetalleCompraService detalleCompraService)
        {
            _comprasService = comprasService;
            _hostEnvironment = hostEnvironment;
            _proveedorService = proveedorService;
            _insumoService = insumoService;
            _productoService = productoService;
            _detalleCompraService = detalleCompraService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _comprasService.ObtenerListaCompras());
        }
        public async Task<IActionResult> DetalleIndex(int Id)
        {
            ViewBag.IdCompra = Id;
            return View(await _detalleCompraService.ObtenerListaDetallePorId(Id));
        }

        [HttpGet]
        public async Task<IActionResult> CrearCompra()
        {
            ViewBag.ListarProveedor = new SelectList(await _proveedorService.ObtenerListaProveedor(), "ProveedorId", "Nombre");
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
                    return RedirectToAction("Index");
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
                    TempData["Accion"] = "Crear";
                    TempData["Mensaje"] = "Compra guardada correctamente";
                    return RedirectToAction("Index");
                    //return RedirectToAction(nameof(CrearDetalle), new { CompraId = compra.CompraId });
                }
                catch (Exception)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Ingresas un valor inválido";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Error realizando la operación";
                return RedirectToAction("Index");
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
                    TempData["Accion"] = "Eliminar";
                    TempData["Mensaje"] = "Compra eliminada correctamente";
                    return RedirectToAction("Index");
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
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Error realizando la operación";
                return RedirectToAction("Index");
            }

        }

        // --------------------------------------------------- DETALLE ---------------------------------------------
        [HttpGet]
        public async Task<IActionResult> CrearDetalle(int Id)
        {
            ViewBag.ListarProducto = new SelectList(await _productoService.ObtenerListaProductos(), "ProductoId", "Nombre");
            ViewBag.ListarInsumo = new SelectList(await _insumoService.ObtenerListaInsumos(), "InsumoId", "Nombre");
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
                    InsumoId = compraDetalleViewModel.InsumoId,
                    Cantidad = compraDetalleViewModel.Cantidad,
                    CantInsumo = compraDetalleViewModel.CantInsumo,
                    CantProducto = compraDetalleViewModel.CantProducto,
                    Costo = compraDetalleViewModel.Costo,
                    Medida = compraDetalleViewModel.Medida,
                    SubTotal = 0,
                    Iva = compraDetalleViewModel.Iva,
                    Total = 0


                };

                try
                {
                    if (compraDetalleViewModel.Cantidad <= 0 || compraDetalleViewModel.CantInsumo <= 0 || compraDetalleViewModel.CantProducto <=0 || compraDetalleViewModel.Costo <=0 || compraDetalleViewModel.Iva <=0)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "Debe ser mayor a 0 las cantidades, el IVA y el costo";
                        return RedirectToAction("Index");
                    }
                    /*var ProductoExiste = await _detalleCompraService.ProductoExiste(detalleCompra.ProductoId);

                    if (ProductoExiste != null)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "El producto ya se encuentra registrado";
                        return View("DetalleIndex");
                    }*/
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
            return RedirectToAction("Index");
        }

        /*[HttpGet]
        public async Task<IActionResult> EditarDetalle(int Id)
        {
            DetalleCompra detalleCompra = await _detalleCompraService.ObtenerDetalleCompraId(Id);
            ViewBag.ListarProducto = new SelectList(await _productoService.ObtenerListaProductos(), "ProductoId", "Nombre");
            ViewBag.IdCompra = Id;
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
        public async Task<IActionResult> EditarDetalle(CompraDetalleViewModel compraDetalleViewModel)
        {
            if (ModelState.IsValid)
            {
                DetalleCompra detalleCompra = new()
                {
                    DetalleCompraId = compraDetalleViewModel.DetalleCompraId,
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

                    await _detalleCompraService.EditarDetalle(detalleCompra);
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

        }*/

    }
}