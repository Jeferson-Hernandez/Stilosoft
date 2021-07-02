using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stilosoft.Business.Abstract;
using Stilosoft.Business.Dtos;
using Stilosoft.Model.Entities;
using Stilosoft.ViewModels.Citas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
    public class CitasController : Controller
    {
        private readonly ICitaService _citaService;
        private readonly IClienteService _clienteService;
        private readonly IServicioService _servicioService;

        public CitasController(ICitaService citaService, IClienteService clienteService, IServicioService servicioService)
        {
            _citaService = citaService;
            _clienteService = clienteService;
            _servicioService = servicioService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _citaService.ObtenerListaCitas());
        }
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            ViewBag.Clientes = new SelectList(await _clienteService.ObtenerListaClientes(), "ClienteId", "Nombre");            
            CitaDetalleDto citaDetalleDto = new();
            citaDetalleDto.Servicios = _servicioService.ObtenerListaServiciosCita();

            return View(citaDetalleDto);
        }
        [HttpPost]
        public async Task<IActionResult> Crear(CitaDetalleDto citaDetalleDto)
        {
            if (ModelState.IsValid)
            {
                var hora = citaDetalleDto.FechaHora.TimeOfDay.ToString();
                long total = 0;
                
                foreach (var servicio in citaDetalleDto.Servicios)
                {
                    if (servicio.Seleccionado==true)
                    {
                        total += servicio.Costo;
                    };
                }
                Cita cita = new()
                {
                    ClienteId = citaDetalleDto.ClienteId,
                    Estado = "Por Aprobar",
                    Fecha = citaDetalleDto.FechaHora.Date,
                    Hora = hora,
                    Total = total
                };
                try
                {
                    await _citaService.GuardarCita(cita); 
                    var citaId = _citaService.ObtenerCitaMaxId();

                    await _citaService.GuardarCitaDetalle(citaId, citaDetalleDto.Servicios);
                    return RedirectToAction("index");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View();
        }
    }
}
