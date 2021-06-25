using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stilosoft.Business.Abstract;
using Stilosoft.Model.Entities;
using Stilosoft.ViewModels.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
    
    public class ServiciosController : Controller
    {
        private readonly IServicioService _servicioService;

        public ServiciosController(IServicioService servicioService)
        {
            _servicioService = servicioService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _servicioService.ObtenerListaServicios());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ServicioViewModel servicioViewModel)
        {
            if (ModelState.IsValid)
            {
                Servicio servicio = new()
                {
                    Nombre = servicioViewModel.Nombre,
                    Duracion = servicioViewModel.Duracion,
                    Costo = servicioViewModel.Costo,
                    Categoria = servicioViewModel.Categoria,
                    Estado = true
                };
                
                try
                {
                    var ServicioExiste = await _servicioService.NombreServicioExiste(servicio.Nombre);

                    if (ServicioExiste != null)
                    {
                        return RedirectToAction("index");
                    }
                    await _servicioService.GuardarServicio(servicio);
                    return RedirectToAction("index");
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

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            Servicio servicio = await _servicioService.ObtenerServicioPorId(id);
            ServicioViewModel servicioViewModel = new()
            {
                ServicioId = servicio.ServicioId,
                Nombre = servicio.Nombre,
                Duracion = servicio.Duracion,
                Costo = servicio.Costo,
                Categoria = servicio.Categoria,
                Estado = servicio.Estado
            };

            return View(servicioViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ServicioViewModel servicioViewModel)
        {
            if (ModelState.IsValid)
            {
                Servicio servicio = new()
                {
                    ServicioId = servicioViewModel.ServicioId,
                    Nombre = servicioViewModel.Nombre,
                    Duracion = servicioViewModel.Duracion,
                    Costo = servicioViewModel.Costo,
                    Categoria = servicioViewModel.Categoria,
                    Estado = servicioViewModel.Estado
                };

                try
                {
                    var ServicioExiste = await _servicioService.NombreServicioExiste(servicio.Nombre);

                    if (ServicioExiste != null)
                    {
                        return RedirectToAction("index");
                    }
                    await _servicioService.EditarServicio(servicio);
                    return RedirectToAction("index");
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

        [HttpPost]
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id != null)
                    {
                        await _servicioService.EliminarServicio(id.Value);
                        return RedirectToAction("index");
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
        [HttpPost]
        public async Task<IActionResult> EditarEstado(int id)
        {
            Servicio servicio = await _servicioService.ObtenerServicioPorId(id);
            if (servicio.Estado == true)
                servicio.Estado = false;
            else if (servicio.Estado == false)
                servicio.Estado = true;

            try
            {
                await _servicioService.EditarServicio(servicio);
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
