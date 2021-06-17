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
