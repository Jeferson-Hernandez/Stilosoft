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
            return await _context.Compra.Include(p=>p.Proveedor).ToListAsync();

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

        public async Task EditarCompra(Compra compra)
        {
            _context.Update(compra);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarCompra(int Id)
        {
            var compra = await ObtenerCompraPorId(Id);
            _context.Remove(compra);
            await _context.SaveChangesAsync();
        }
    }
}
