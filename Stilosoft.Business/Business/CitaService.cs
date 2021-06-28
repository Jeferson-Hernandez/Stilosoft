﻿using Microsoft.EntityFrameworkCore;
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
    public class CitaService:ICitaService
    {
        private readonly AppDbContext _context;

        public CitaService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Cita>> ObtenerListaCitas()
        {
            return await _context.Cita.Include(c=>c.Cliente).ToListAsync();
        }
        public async Task<Cita> ObtenerCitaPorId(int id)
        {
            return await _context.Cita.Include(c => c.Cliente).FirstOrDefaultAsync(c => c.CitaId == id);
        }
        public async Task GuardarCita(Cita cita)
        {
            _context.Add(cita);
            await _context.SaveChangesAsync();
        }
        public async Task EliminarCita(int id)
        {
            var cita = await ObtenerCitaPorId(id);
            _context.Remove(cita);
            await _context.SaveChangesAsync();
        }

    }
}
