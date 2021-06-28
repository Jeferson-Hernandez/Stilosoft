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

        public async Task<IEnumerable<Insumo>> ObtenerListaInsumos()
        {
            return await _context.Insumo.ToListAsync();
        }
        public async Task<Insumo> ObtenerInsumoPorId(int Id)
        {
            return await _context.Insumo.FirstOrDefaultAsync(i => i.InsumoId == Id);
        }
        public async Task RegistrarInsumo(Insumo insumo)
        {
            _context.Add(insumo);
            await _context.SaveChangesAsync();
        }
        public async Task EditarInsumo(Insumo insumo)
        {
            _context.Update(insumo);
            await _context.SaveChangesAsync();
        }
        public async Task EliminarInsumo(int Id)
        {
            var insumo = await ObtenerInsumoPorId(Id);
            _context.Remove(insumo);
            await _context.SaveChangesAsync();
        }
        public async Task<Insumo> NombreInsumoExiste(string Nombre)
        {
            return await _context.Insumo.FirstOrDefaultAsync(i => i.Nombre == Nombre);
        }


    }
}
