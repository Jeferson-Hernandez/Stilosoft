using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Stilosoft.Business.Abstract;
using Stilosoft.Model.Entities;
using Stilosoft.ViewModels.Proveedor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
    [Authorize]
    public class ProveedorController : Controller
    {
        private readonly IProveedorService _proveedorService;

        public ProveedorController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _proveedorService.ObtenerListaProveedor());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(ProveedorViewModels proveedorViewModels)
        {
            if (ModelState.IsValid)
            {
                Proveedor proveedor = new()
                {
                    Nit = proveedorViewModels.Nit,
                    Nombre = proveedorViewModels.Nombre,
                    Direccion = proveedorViewModels.Direccion,
                    Telefono = proveedorViewModels.Telefono,
                    Contacto = proveedorViewModels.Contacto,                    
                    TelefonoContacto = proveedorViewModels.TelefonoContacto,                    
                    Estado = true
                };
                try
                {
                    var NitExiste = await _proveedorService.NitProveedorExiste(proveedor.Nit);
                    if (NitExiste != null)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "El NIT ya se encuentra registrado";
                        return View(proveedorViewModels);
                    }                    
                    await _proveedorService.GuardarProveedor(proveedor);
                    TempData["Accion"] = "Crear";
                    TempData["Mensaje"] = "El NIT se ha registrado";
                    return RedirectToAction("index");
                }
                catch (Exception)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Ingresaste un valor inválido";
                    return RedirectToAction("index");
                }
            }
            TempData["Accion"] = "Error";
            TempData["Mensaje"] = "Ingresaste un valor inválido";
            return View(proveedorViewModels);
        }
        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Error";
                return RedirectToAction("index");
            }
            Proveedor proveedor = await _proveedorService.ObtenerProveedorPorId(id.Value);
            ProveedorViewModels proveedorViewModels = new()
            {
                Nit = proveedor.Nit,
                Nombre = proveedor.Nombre,
                Direccion = proveedor.Direccion,
                Telefono = proveedor.Telefono,
                Contacto = proveedor.Contacto,
                TelefonoContacto = proveedor.TelefonoContacto,
                Estado = proveedor.Estado,
            };
            return View(proveedorViewModels);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(ProveedorViewModels proveedorViewModels)
        {
            if (ModelState.IsValid)
            {
                Proveedor proveedor = new()
                {
                    Nit = proveedorViewModels.Nit,
                    Nombre = proveedorViewModels.Nombre,
                    Direccion = proveedorViewModels.Direccion,
                    Telefono = proveedorViewModels.Telefono,
                    Contacto = proveedorViewModels.Contacto,
                    TelefonoContacto = proveedorViewModels.TelefonoContacto,
                    Estado = proveedorViewModels.Estado
                };
                await _proveedorService.EditarProveedor(proveedor);
                TempData["Accion"] = "Editar";
                TempData["Mensaje"] = "Estilista editado correctamente";
                return RedirectToAction("index");
            }
            TempData["Accion"] = "Error";
            TempData["Mensaje"] = "Ingresaste un valor inválido";
            return View(proveedorViewModels);
        }       
    }
}
