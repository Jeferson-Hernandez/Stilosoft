using Microsoft.EntityFrameworkCore;
using Stilosoft.Business.Abstract;
using Stilosoft.Business.Dtos;
using Stilosoft.Business.Dtos.Cita;
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

        public async Task<Servicio> ObtenerServicioPorId(int id)
        {
            return await _context.Servicio.FirstOrDefaultAsync(s => s.ServicioId == id);
        }

        public async Task GuardarServicio(Servicio servicio)
        {
            _context.Add(servicio);
            await _context.SaveChangesAsync();            
        }

        public async Task EditarServicio(Servicio servicio)
        {
            _context.Update(servicio);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarServicio(int id)
        {
            var servicio = await ObtenerServicioPorId(id);
            _context.Remove(servicio);
            await _context.SaveChangesAsync();
        }
        public async Task<Servicio> NombreServicioExiste(string nombre)
        {
            return await _context.Servicio.FirstOrDefaultAsync(n => n.Nombre == nombre);
        }
        public List<CitaServiciosDto> ObtenerListaServiciosCita()
        {
            List<CitaServiciosDto> listaServiciosDtos = new();
            _context.Servicio.ToList().ForEach(s =>
            {
                CitaServiciosDto citaServiciosDto = new()
                {
                    ServicioId = s.ServicioId,
                    Nombre = s.Nombre,
                    Duracion = s.Duracion
                };
                listaServiciosDtos.Add(citaServiciosDto);
            });
            return listaServiciosDtos;
        }
    }
}
