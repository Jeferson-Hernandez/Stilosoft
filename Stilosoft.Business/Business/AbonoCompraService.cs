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
    public class AbonoCompraService: IAbonoCompraService
    {
        private readonly AppDbContext _context;

        public AbonoCompraService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AbonoCompra>> ObtenerListaAbonoCompra()
        {
            return await _context.AbonoCompra.Include(c=>c.Compra).ToListAsync();
        }
        public async Task<AbonoCompra> ObtenerAbonoCompraPorId(int Id)
        {
            return await _context.AbonoCompra.FirstOrDefaultAsync(s => s.AbonoCompraId == Id);
        }
        public async Task GuardarAbonoCompra(AbonoCompra abonoCompra)
        {
            _context.Add(abonoCompra);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAbonoCompra(int id)
        {
            var abonoCompra = await ObtenerAbonoCompraPorId(id);
            _context.Remove(abonoCompra);
            await _context.SaveChangesAsync();
        }
    }
}
