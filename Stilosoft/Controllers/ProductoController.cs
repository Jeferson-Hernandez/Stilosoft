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
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductoController(IProductoService productoService, IWebHostEnvironment hostEnvironment)
        {
            _productoService = productoService;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _productoService.ObtenerListaProductos());
        }

        [HttpGet]
        public IActionResult CrearPro()
        {       
                return View(new ProductoViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> CrearPro(ProductoViewModel productoViewModel)
        {
            //preguntamos si el modelo es válido o no (comprueba validaciones)
            if (ModelState.IsValid)
            {
                // se crea un objeto de tipo empleado y se le asignan las propiedades que vienen de empleadoViewModel
                Producto producto = new()
                {
                    Nombre = productoViewModel.Nombre,
                    Cantidad = productoViewModel.Cantidad,
                    Categoria = productoViewModel.Categoria,
                    Precio = productoViewModel.Precio,
                };
                if (producto.Cantidad <= 0)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "La cantidad debe ser mayor a 0";
                    return RedirectToAction("index");
                }
                else if (producto.Precio <= 0)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "El precio debe ser mayor a 0";
                    return RedirectToAction("index");
                }
                string path = null;
                string wwwRootPath = null;

                // si se utiliza una imagen entonces
                if (productoViewModel.Imagen != null)
                {
                    //obtenemos la ruta raiz de nuestro proyecto
                    wwwRootPath = _hostEnvironment.WebRootPath;
                    //obtenemos el nombre de la imagen
                    string nombreImagen = Path.GetFileNameWithoutExtension(productoViewModel.Imagen.FileName);
                    //obtenemos la extensión de la imagen .jpg - .png etc
                    string extension = Path.GetExtension(productoViewModel.Imagen.FileName);
                    //concatenamos el nombre de la imagen con el año-minuto-segundos-fraciones de segundo + la extensión
                    producto.RutaImagen = nombreImagen + DateTime.Now.ToString("yymmssfff") + extension;
                    //Obetenemos la ruta en donde vamos a guardar la imagen
                    path = Path.Combine(wwwRootPath + "/imagenes/" + producto.RutaImagen);
                }
                    try
                    {
                        // si se va a guardar una imagen
                        if (path != null)
                        {
                            //copiamos la imagen a la ruta especifica
                            using var fileStream = new FileStream(path, FileMode.Create);
                            await productoViewModel.Imagen.CopyToAsync(fileStream);
                        }

                        await _productoService.RegistrarProducto(producto);
                        TempData["Accion"] = "RegistrarProducto";
                        TempData["Mensaje"] = "Producto guardado con éxito";
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
                return View(productoViewModel);
            }
        }
        [HttpGet]      
        public async Task<IActionResult> EditarProducto(int id)
        {     
            Producto producto = await _productoService.ObtenerProductoPorId(id);
            ProductoViewModel productoViewModel = new()
            {
                ProductoId = producto.ProductoId,
                Nombre = producto.Nombre,
                Cantidad = producto.Cantidad,
                Categoria = producto.Categoria,
                Precio = producto.Precio,
                RutaImagen = producto.RutaImagen
            };
            return View(productoViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditarProducto(ProductoViewModel productoViewModel)
        {
            {
                if (ModelState.IsValid)
                {
                    Producto producto = new()
                    {
                        ProductoId = productoViewModel.ProductoId,
                        Nombre = productoViewModel.Nombre,
                        Cantidad = productoViewModel.Cantidad,
                        Categoria = productoViewModel.Categoria,
                        Precio = productoViewModel.Precio
                    };
                    if (producto.Cantidad <= 0)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "La cantidad debe ser mayor a 0";
                        return RedirectToAction("index");
                    }
                    else if (producto.Precio <= 0)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "El precio debe ser mayor a 0";
                        return RedirectToAction("index");
                    }
                    string wwwRootPath = null;
                    string path = null;

                    if (productoViewModel.Imagen != null)
                    {
                        wwwRootPath = _hostEnvironment.WebRootPath;
                        string nombreImagen = Path.GetFileNameWithoutExtension(productoViewModel.Imagen.FileName);
                        string extension = Path.GetExtension(productoViewModel.Imagen.FileName);
                        producto.RutaImagen = nombreImagen + DateTime.Now.ToString("yymmssfff") + extension;
                        path = Path.Combine(wwwRootPath + "/imagenes/" + producto.RutaImagen);
                    }

                    try
                    {
                        if (path != null)
                        {
                            using var fileStream = new FileStream(path, FileMode.Create);
                            await productoViewModel.Imagen.CopyToAsync(fileStream);
                            if (productoViewModel.RutaImagen != null)
                            {
                                FileInfo file = new FileInfo(wwwRootPath + "/imagenes/" + productoViewModel.RutaImagen);
                                file.Delete();
                            }
                        }
                        else
                        {
                            producto.RutaImagen = productoViewModel.RutaImagen;
                        }
                    

                        await _productoService.EditarProducto(producto);
                        TempData["Accion"] = "EditarProducto";
                        TempData["Mensaje"] = "Producto editado correctamente";
                        return RedirectToAction("index");
                    }
                    catch (Exception)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "Ocurrió un error";
                        return RedirectToAction("index");
                    }

                }
                else
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Ocurrió un error";
                    return View(productoViewModel);
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Producto producto = await _productoService.ObtenerProductoPorId(id);

                    if (producto.RutaImagen != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        FileInfo file = new FileInfo(wwwRootPath + "/imagenes/" + producto.RutaImagen);
                        file.Delete();
                    }

                    await _productoService.EliminarProducto(id);
                    TempData["Accion"] = "EliminarProducto";
                    TempData["Mensaje"] = "Producto eliminado correctamente";
                    return RedirectToAction("index");
                }
                catch (Exception)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Ocurrió un error";
                    return RedirectToAction("index");
                }
            }
            else
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Ocurrió un error";
                return RedirectToAction("index");
            }

        }

    }
}
