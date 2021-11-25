using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stilosoft.Business.Abstract;
using Stilosoft.Model.DAL;
using Stilosoft.ViewModels.SolicitudServicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
    public class SolicitudServicioController : Controller
    {
        private readonly ISolicitudService _solicitudService;
        private readonly IProductoService _productoService;
        private readonly IServicioService _servicioService;
        private readonly IClienteService _clienteService;
        private readonly AppDbContext _context;

        public SolicitudServicioController(ISolicitudService solicitudService, IProductoService productoService, IServicioService servicioService, IClienteService clienteService, AppDbContext context)
        {
            _solicitudService = solicitudService;
            _productoService = productoService;
            _servicioService = servicioService;
            _clienteService = clienteService;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _solicitudService.ObtenerListaSolicitudes());
        }
        [HttpGet]
        public async Task<IActionResult> CrearSolicitud()
        {
            var cliente = await _context.Cliente.Include(u => u.IdentityUser).Where(e => e.Estado == true).Select(s => new
            {
                clienteId = s.ClienteId,
                datosCliente = string.Format("{0} {1} - {2}", s.Nombre, s.Apellido, s.Documento)
            }).ToListAsync();

            ViewBag.Clientes = new SelectList(cliente, "clienteId", "datosCliente");
            SolicitudServicioViewModel solicitud = new();
            solicitud.servicios = await _servicioService.ObtenerListaServiciosSolicitud();
            solicitud.productos = await _productoService.ObtenerListaProductosSolicitud();
            
            return View(solicitud);
        }
    }
}
