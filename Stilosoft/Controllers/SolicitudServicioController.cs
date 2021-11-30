using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stilosoft.Business.Abstract;
using Stilosoft.Model.DAL;
using Stilosoft.Model.Entities;
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
        private readonly IEstilistaService _estilistaService;
        private readonly AppDbContext _context;

        public SolicitudServicioController(ISolicitudService solicitudService, IProductoService productoService, IServicioService servicioService, IClienteService clienteService, IEstilistaService estilistaService, AppDbContext context)
        {
            _solicitudService = solicitudService;
            _productoService = productoService;
            _servicioService = servicioService;
            _clienteService = clienteService;
            _estilistaService = estilistaService;
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
                ClienteId = s.ClienteId,
                DatosCliente = string.Format("{0} {1} - {2}", s.Nombre, s.Apellido, s.Documento)
            }).ToListAsync();

            ViewBag.Clientes = new SelectList(cliente, "ClienteId", "DatosCliente");
            ViewBag.Estilistas = new SelectList(await _estilistaService.ObtenerListaEstilistasEstado(), "EstilistaId", "Nombre");
            SolicitudServicioViewModel solicitud = new();
            solicitud.Servicios = await _servicioService.ObtenerListaServiciosSolicitud();
            solicitud.Productos = await _productoService.ObtenerListaProductosSolicitud();
            return View(solicitud);
        }
        [HttpPost]
        public IActionResult CrearSolicitud(SolicitudServicioViewModel solicitud)
        {
            if (ModelState.IsValid)
            {
                using(var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var totalCompra = solicitud.TotalProductos + solicitud.TotalServicios;
                        SolicitudServicio solicitudServicio = new()
                        {
                            ClienteId = solicitud.ClienteId,
                            Fecha = solicitud.FechaHora.Date,
                            Hora = solicitud.FechaHora.TimeOfDay.ToString(),
                            Total = totalCompra,
                            Estado = "Activa"
                        };
                        _context.Add(solicitudServicio);
                        _context.SaveChanges();

                        foreach (var servicio in solicitud.ServiciosSolicitud)
                        {
                            DetalleServicioServicios detalleServicios = new()
                            {
                                SolicitudServicioId = solicitudServicio.SolicitudServicioId,
                                ServicioId = servicio.ServicioId,
                                EstilistaId = servicio.EstilistaId,
                                Precio = servicio.Precio
                            };
                            _context.Add(detalleServicios);
                        }
                        _context.SaveChanges();

                        if (solicitud.ProductosSolicutud.Count() > 0)
                        {
                            foreach (var producto in solicitud.ProductosSolicutud)
                            {
                                DetalleServicioProductos detalleProductos = new()
                                {
                                    SolicitudServicioId = solicitudServicio.SolicitudServicioId,
                                    ProductoId = producto.ProductoId,
                                    Cantidad = producto.Cantidad,
                                    Precio = producto.Precio
                                };
                                _context.Add(detalleProductos);
                            }
                            _context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "No se pudo completar la operación";
                        return RedirectToAction("index");
                    }
                }
                TempData["Accion"] = "Crear";
                TempData["Mensaje"] = "Solicitud creada correctamente";
                return RedirectToAction("index");
            }
            TempData["Accion"] = "Error";
            TempData["Mensaje"] = "Ingresaste un valor inválido";
            return RedirectToAction("index");
        }
    }
}
