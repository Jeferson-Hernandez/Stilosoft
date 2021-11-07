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
    public class ProveedorService: IProveedorService
    {
        private readonly AppDbContext _context;

        public ProveedorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proveedor>> ObtenerListaProveedor()
        {
            return await _context.Proveedor.ToListAsync();
        }

        public async Task<Proveedor> ObtenerProveedorPorId(int id)
        {
            return await _context.Proveedor.FirstOrDefaultAsync(s => s.ProveedorId == id);
        }

        public async Task GuardarProveedor(Proveedor proveedor)
        {
            _context.Add(proveedor);
            await _context.SaveChangesAsync();
        }

        public async Task EditarProveedor(Proveedor proveedor)
        {
            _context.Update(proveedor);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarProveedor(int id)
        {
            var proveedor = await ObtenerProveedorPorId(id);
            _context.Remove(proveedor);
            await _context.SaveChangesAsync();
        }

        public async Task<Proveedor> NitProveedorExiste(string Nit)
        {
            return await _context.Proveedor.FirstOrDefaultAsync(n => n.Nit == Nit);
        }
    }
}
    

