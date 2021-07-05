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
        private readonly IComprasService _comprasService;

        public AbonoCompraService(AppDbContext context, IComprasService comprasService)
        {
            _context = context;
            _comprasService = comprasService;
        }
        public async Task<IEnumerable<AbonoCompra>> ObtenerListaAbonoCompra()
        {
            return await _context.AbonoCompra.Include(c=>c.Compra).ToListAsync();
        }
        public async Task<IEnumerable<AbonoCompra>> ObtenerListaAbonoPorId(int Id)
        {
            return await _context.AbonoCompra.Include(c => c.Compra).Where(s => s.CompraId == Id).ToListAsync();
        }
        public long ObtenerAbonoPorId(int Id)
        {
            return  _context.AbonoCompra.Where(c => c.CompraId == Id).Min(p => p.PrecioTotal);
        }
        public int ObtenerCuotasPorId(int Id)
        {
            return _context.AbonoCompra.Where(ci => ci.CompraId == Id).Min(c => c.Cuotas);
        }
        public async Task GuardarAbonoCompra(AbonoCompra abonoCompra)
        {
            _context.Add(abonoCompra);
            await _context.SaveChangesAsync();
        }
        public async Task<AbonoCompra> ObtenerAbonoCompraId(int Id)
        {
            return await _context.AbonoCompra.FirstOrDefaultAsync(e => e.AbonoCompraId == Id);
        }
        public async Task EliminarAbonoCompra(int Id)
        {
            var abonoCompra = await ObtenerAbonoCompraId(Id);
            _context.Remove(abonoCompra);
            await _context.SaveChangesAsync();
        }
    }
}
