using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Model.DAL
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):
            base(options)
        {

        }
        public DbSet<Servicio> Servicio { get; set; }
        public DbSet<Insumo> Insumo { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Estilista> Estilista { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Cita> Cita { get; set; }
        public DbSet<AbonoCompra> AbonoCompra { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<DetalleCita> DetalleCita { get; set; }
        public DbSet<DetalleCompra> DetalleCompra { get; set; }
        public DbSet<DetalleServicioInsumo> DetalleServicioInsumo { get; set; }
        public DbSet<DetalleServicioProductos> DetalleServicioProductos { get; set; }
        public DbSet<DetalleServicioServicios> DetalleServicioServicios { get; set; }
        public DbSet<SolicitudServicio> SolicitudServicio { get; set; }
    }
}
