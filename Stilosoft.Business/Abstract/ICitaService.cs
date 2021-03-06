using Stilosoft.Business.Dtos.Cita;
using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Business.Abstract
{
    public interface ICitaService
    {
        Task<IEnumerable<Cita>> ObtenerListaCitas();
        Task<Cita> ObtenerCitaPorId(int id);
        Task GuardarCita(Cita cita);
        Task EliminarCita(int id);
        int ObtenerCitaMaxId();
        Task GuardarCitaDetalle(int citaId, List<CitaServiciosDto> citaServiciosDtos);
        List<CitaDetalleAsignarEstilistaDto> ObtenerListaDetalleCitaPorId(int id);
        Task GuardarCitaDetalleEstilista(List<CitaDetalleAsignarEstilistaDto> asignarEstilista);

    }
}
