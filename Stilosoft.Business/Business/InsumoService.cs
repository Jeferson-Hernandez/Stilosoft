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
    public class InsumoService:IInsumoService
    {
      
            private readonly AppDbContext _context;
            public InsumoService(AppDbContext context)
            {
                _context = context;
            }


        public async Task<IEnumerable<Insumo>> ObtenerListaInsumo()
            {
                return await _context.Insumo.ToListAsync();
            }
        }
}
