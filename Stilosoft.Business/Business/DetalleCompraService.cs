using Microsoft.EntityFrameworkCore;
using Stilosoft.Business.Abstract;
using Stilosoft.Model.DAL;
using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Business.Business
{
    public class DetalleCompraService: IDetalleCompraService
    {
        private readonly AppDbContext _context;
        private readonly IComprasService _comprasService;

        public DetalleCompraService(AppDbContext context, IComprasService comprasService)
        {
            _context = context;
            _comprasService = comprasService;
        }
        public async Task<IEnumerable<DetalleCompra>> ObtenerDetalleCompras()
        {
            return await _context.DetalleCompra.Include(p => p.Producto).Include(c => c.Compra).ToListAsync();

        }
        public async Task<IEnumerable<DetalleCompra>> ObtenerListaDetallePorId(int Id)
        {
            return await _context.DetalleCompra.Include(p => p.Producto).Include(c => c.Compra).Where(s => s.CompraId == Id).ToListAsync();
        }
        public async Task RegistrarDetalleCompra(DetalleCompra detalleCompra)
        {
            _context.Add(detalleCompra);
            await _context.SaveChangesAsync();
        }
        public async Task<DetalleCompra> ObtenerDetalleCompraId(int Id)
        {
            return await _context.DetalleCompra.FirstOrDefaultAsync(e => e.DetalleCompraId == Id);
        }
        public async Task EditarDetalle(DetalleCompra detalleCompra)
        {
            _context.Update(detalleCompra);
            await _context.SaveChangesAsync();
        }
        public async Task EliminarDetalleCompra(int Id)
        {
            var detalleCompra = await ObtenerDetalleCompraId(Id);
            _context.Remove(detalleCompra);
            await _context.SaveChangesAsync();
        }

        public async Task<DetalleCompra> ProductoExiste(int Producto)
        {
            return await _context.DetalleCompra.FirstOrDefaultAsync(n => n.ProductoId == Producto);
        }
    }
}
