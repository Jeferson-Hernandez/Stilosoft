using Microsoft.EntityFrameworkCore;
using Stilosoft.Business.Abstract;
using Stilosoft.Model.DAL;
using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Business.Business
{
    public class ComprasService:IComprasService
    {
        private readonly AppDbContext _context;

        public ComprasService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Compra>> ObtenerListaCompras()
        {
            return await _context.Compra.Include(p=>p.Proveedor).Include(d => d.DetalleCompras).ToListAsync();

        }
        public async Task<Compra> ObtenerCompraPorId(int Id)
        {
            return await _context.Compra.FirstOrDefaultAsync(c => c.CompraId == Id);
        }
        public async Task RegistrarCompra(Compra compra)
        {
            _context.Add(compra);
            await _context.SaveChangesAsync();
        }

        public async Task EditarDetalleCompra(DetalleCompra detalleCompra)
        {
            _context.Update(detalleCompra);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarCompra(int Id)
        {
            var compra = await ObtenerCompraPorId(Id);
            _context.Remove(compra);
            await _context.SaveChangesAsync();
        }

        // DETALLE

        public async Task<IEnumerable<DetalleCompra>> ObtenerDetalleCompras()
        {
            return await _context.DetalleCompra.Include(p => p.Producto).Include(c => c.Compra).ToListAsync();

        }

        public async Task RegistrarDetalleCompra(DetalleCompra detalleCompra)
        {
            _context.Add(detalleCompra);
            await _context.SaveChangesAsync();
        }

        public async Task<DetalleCompra> ObtenerDetalleCompraPorId(int Id)
        {
            return await _context.DetalleCompra.FirstOrDefaultAsync(c => c.DetalleCompraId == Id);
        }

        public async Task EliminarDetalleCompra(int Id)
        {
            var detallecompra = await ObtenerDetalleCompraPorId(Id);
            _context.Remove(detallecompra);
            await _context.SaveChangesAsync();
        }

        public async Task<Compra> NoFacturaExiste(string NoFactura)
        {
            return await _context.Compra.FirstOrDefaultAsync(n => n.NoFactura == NoFactura);
        }

        public async Task<DetalleCompra> ProductoExiste(int Producto)
        {
            return await _context.DetalleCompra.FirstOrDefaultAsync(n => n.ProductoId == Producto);
        }
    }
}
