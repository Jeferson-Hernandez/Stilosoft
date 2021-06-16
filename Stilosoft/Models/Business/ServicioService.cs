using Microsoft.EntityFrameworkCore;
using Stilosoft.Models.Abstract;
using Stilosoft.Models.DAL;
using Stilosoft.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Models.Business
{
    public class ServicioService:IServicioService
    {
        private readonly AppDbContext _context;

        public ServicioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Servicio>> ObtenerListaServicios()
        {
            return await _context.Servicio.ToListAsync();
        }

    }
}
