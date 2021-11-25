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
    public class SolicitudService:ISolicitudService
    {
        private readonly AppDbContext _context;

        public SolicitudService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SolicitudServicio>> ObtenerListaSolicitudes()
        {
            return await _context.SolicitudServicio.Include(c => c.Cliente).ToListAsync();
        }
    }
}
