using Microsoft.EntityFrameworkCore;
using Stilosoft.Business.Abstract;
using Stilosoft.Business.Dtos.Cita;
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
        public int ObtenerCitaMaxId()
        {
            return _context.Cita.Max(c => c.CitaId);
        }
        //Relacionado al crear la cita y guardar lo ingresado al detalle
        public async Task GuardarCitaDetalle(int citaId, List<CitaServiciosDto> citaServiciosDtos)
        {
            foreach (var servicios in citaServiciosDtos)
            {
                if (servicios.Seleccionado==true)
                {
                    DetalleCita detalleCita = new()
                    {
                        CitaId = citaId,
                        ServicioId = servicios.ServicioId
                    };
                    await GuardarCitaDetalle(detalleCita);
                }
            }
        }
        public async Task GuardarCitaDetalle(DetalleCita detalleCita)
        {
            _context.Add(detalleCita);
            await _context.SaveChangesAsync();
        }
        //Relacionado con la selección del estilista para asignarlo al detalle de la cita
        public List<CitaDetalleAsignarEstilistaDto> ObtenerListaDetalleCitaPorId(int id)
        {
            List<CitaDetalleAsignarEstilistaDto> ListaCitaDetalle = new();
            _context.DetalleCita.Include(s => s.Servicio).Where(c => c.CitaId == id).ToList().ForEach(s =>
              {
                  CitaDetalleAsignarEstilistaDto citaDetalleEstilista = new()
                  {
                      CitaId = s.CitaId,
                      DetalleCitaId = s.DetalleCitaId,
                      NombreServicio = s.Servicio.Nombre,
                      ServicioId = s.ServicioId
                  };
                  ListaCitaDetalle.Add(citaDetalleEstilista);
              });
            return ListaCitaDetalle;            
        }
        public async Task GuardarCitaDetalleEstilista(List<CitaDetalleAsignarEstilistaDto> asignarEstilista)
        {
            foreach (var estilista in asignarEstilista)
            {                
               DetalleCita detalleCita = new()
               {
                  DetalleCitaId = estilista.DetalleCitaId,
                  CitaId = estilista.CitaId,
                  ServicioId = estilista.ServicioId,
                  EstilistaId = (int)estilista.EstilistaId
               };
               await AsignarEstilistaDetalle(detalleCita);                
            }
        }
        public async Task AsignarEstilistaDetalle(DetalleCita detalleCita)
        {
            _context.Update(detalleCita);
            await _context.SaveChangesAsync();
        }
    }
}
