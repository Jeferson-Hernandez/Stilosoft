using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stilosoft.Business.Abstract;
using Stilosoft.Business.Dtos;
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
        public IActionResult Crear(CitaViewModel citaViewModel)
        {     

            return View();
        }
    }
}
