using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stilosoft.Business.Abstract;
using Stilosoft.Business.Dtos;
using Stilosoft.Business.Dtos.Cita;
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
        private readonly IEstilistaService _estilistaService;

        public CitasController(ICitaService citaService, IClienteService clienteService, IServicioService servicioService, IEstilistaService estilistaService)
        {
            _citaService = citaService;
            _clienteService = clienteService;
            _servicioService = servicioService;
            _estilistaService = estilistaService;
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
        [HttpGet]
        public async Task<IActionResult> CitaDetalleEstilistas(int id)
        {
            ViewBag.Estilistas = new SelectList(await _estilistaService.ObtenerListaEstilistas(), "EstilistaId", "Nombre");

            CitaDetalleEstilistaDto citaDetalleEstilista = new();
            citaDetalleEstilista.Estilistas = _citaService.ObtenerListaDetalleCitaPorId(id);

            return View(citaDetalleEstilista);
        }
        [HttpPost]
        public async Task<IActionResult> CitaDetalleEstilistas(CitaDetalleEstilistaDto citaDetalleEstilistaDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _citaService.GuardarCitaDetalleEstilista(citaDetalleEstilistaDto.Estilistas);
                    return RedirectToAction("index");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View(citaDetalleEstilistaDto);
        }
    }
}
