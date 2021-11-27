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
    public class EstilistaService : IEstilistaService
    {
        private readonly AppDbContext _context;

        public EstilistaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estilista>> ObtenerListaEstilistas()
        {
            return await _context.Estilista.ToListAsync();
        }
        public async Task<Estilista> ObtenerEstilistaPorId(int id)
        {
            return await _context.Estilista.FirstOrDefaultAsync(e => e.EstilistaId == id);
        }
        public async Task GuardarEstilista(Estilista estilista)
        {
            _context.Add(estilista);
            await _context.SaveChangesAsync();
        }
        public async Task EditarEstilista(Estilista estilista)
        {
            _context.Update(estilista);
            await _context.SaveChangesAsync();
        }
        public async Task EliminarEstilista(int id)
        {
            var estilista = await ObtenerEstilistaPorId(id);
            _context.Remove(estilista);
            await _context.SaveChangesAsync();
        }
        public async Task<Estilista> CedulaEstilistaExiste(string cedula)
        {
            return await _context.Estilista.FirstOrDefaultAsync(n => n.Cedula == cedula);
        }
        public async Task<IEnumerable<Estilista>> CedulaEstilistaExisteEditar(string cedula)
        {            
            return await _context.Estilista.Where(c => c.Cedula == cedula).ToListAsync();
        }
        public async Task<IEnumerable<Estilista>> ObtenerListaEstilistasEstado()
        {
            return await _context.Estilista.Where(s => s.Estado == true).ToListAsync();
        }
    }
}
