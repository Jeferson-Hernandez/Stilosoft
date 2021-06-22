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
    public class EstilistaService:IEstilistaService
    {
        private readonly AppDbContext _context;

        public EstilistaService(AppDbContext context)
        {
            _context = context;
        }

        /*public async Task<IEnumerable<Estilista>> ObtenerListaEstilistas()
        {
            
        }*/

    }
}
