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
    public class ProveedorService:IProveedorService
    {
        private readonly AppDbContext _context;
        public ProveedorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Proveedor>> ObtenerListaProveedores()
        {
            return await _context.Proveedor.ToListAsync();
        }
    }
}
