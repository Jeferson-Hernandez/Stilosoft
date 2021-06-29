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

namespace Stilosoft.Controllers
{
    public class ComprasController : Controller
    {
        private readonly IComprasService _comprasService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ComprasController(IComprasService comprasService, IWebHostEnvironment hostEnvironment)
        {
            _comprasService = comprasService;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _comprasService.ObtenerListaCompras());
        }

        [HttpGet]
        public IActionResult CrearCompra()
        {
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
                    Cantidad = comprasViewModel.Cantidad,
                    PrecioTotal = comprasViewModel.PrecioTotal,
                    FechaFactura = comprasViewModel.FechaFactura,
                    NoFactura = comprasViewModel.NoFactura,
                    FormaPago = comprasViewModel.FormaPago,
                    FechaInicioPago = comprasViewModel.FechaInicioPago,
                    FechaRegistro = comprasViewModel.FechaRegistro,
                    Periodicidad = comprasViewModel.Periodicidad,
                    Cuotas = comprasViewModel.Cuotas
                };
                if (compra.Cantidad <= 0)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "La cantidad debe ser mayor a 0";
                    return RedirectToAction("Index");
                }
                else if (compra.Cuotas <= 0)
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
                return View(comprasViewModel);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditarCompra(int Id)
        {
            Compra compra = await _comprasService.ObtenerCompraPorId(Id);
            ComprasViewModel comprasViewModel = new()
            {
                CompraId = compra.CompraId,
                ProveedorId = compra.ProveedorId,
                Cantidad = compra.Cantidad,
                FechaFactura = compra.FechaFactura,
                NoFactura = compra.NoFactura,
                FormaPago = compra.FormaPago,
                FechaInicioPago = compra.FechaInicioPago,
                FechaRegistro = compra.FechaRegistro,
                Periodicidad = compra.Periodicidad,
                Cuotas = compra.Cuotas,
                RutaImagen = compra.RutaImagen
            };
            return View(comprasViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditarCompra(ComprasViewModel comprasViewModel)
        {
            {
                if (ModelState.IsValid)
                {
                    Compra compra = new()
                    {
                        CompraId = comprasViewModel.CompraId,
                        ProveedorId = comprasViewModel.ProveedorId,
                        Cantidad = comprasViewModel.Cantidad,
                        FechaFactura = comprasViewModel.FechaFactura,
                        NoFactura = comprasViewModel.NoFactura,
                        FormaPago = comprasViewModel.FormaPago,
                        FechaInicioPago = comprasViewModel.FechaInicioPago,
                        FechaRegistro = comprasViewModel.FechaRegistro,
                        Periodicidad = comprasViewModel.Periodicidad,
                        Cuotas = comprasViewModel.Cuotas
                    };
                    if (compra.Cantidad <= 0)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "La cantidad debe ser mayor a 0";
                        return RedirectToAction("index");
                    }
                    else if (compra.Cuotas <= 0)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "Las cuotas deben ser mayor a 0";
                        return RedirectToAction("index");
                    }
                    string wwwRootPath = null;
                    string path = null;

                    if (comprasViewModel.Imagen != null)
                    {
                        wwwRootPath = _hostEnvironment.WebRootPath;
                        string NombreImagen = Path.GetFileNameWithoutExtension(comprasViewModel.Imagen.FileName);
                        string Extension = Path.GetExtension(comprasViewModel.Imagen.FileName);
                        compra.RutaImagen = NombreImagen + DateTime.Now.ToString("yymmssfff") + Extension;
                        path = Path.Combine(wwwRootPath + "/imagenes/" + compra.RutaImagen);
                    }

                    try
                    {
                        if (path != null)
                        {
                            using var fileStream = new FileStream(path, FileMode.Create);
                            await comprasViewModel.Imagen.CopyToAsync(fileStream);
                            if (comprasViewModel.RutaImagen != null)
                            {
                                FileInfo file = new FileInfo(wwwRootPath + "/imagenes/" + comprasViewModel.RutaImagen);
                                file.Delete();
                            }
                        }
                        else
                        {
                            compra.RutaImagen = comprasViewModel.RutaImagen;
                        }


                        await _comprasService.EditarCompra(compra);
                        TempData["Accion"] = "EditarCompra";
                        TempData["Mensaje"] = "Compra registrada correctamente";
                        return RedirectToAction("index");
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
                    return View(comprasViewModel);
                }
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

    }
}