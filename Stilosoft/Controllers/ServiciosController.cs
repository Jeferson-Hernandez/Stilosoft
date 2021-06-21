using Microsoft.AspNetCore.Mvc;
using Stilosoft.Business.Abstract;
using Stilosoft.Model.Entities;
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
        public async Task<IActionResult> Crear(Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                servicio.Estado = true;

                try
                {
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
            return View(servicio);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Servicio servicio)
        {
            if (ModelState.IsValid)
            {
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
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _servicioService.EliminarServicio(id);
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
    }
}
