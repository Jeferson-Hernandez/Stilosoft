using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Business.Abstract
{
    public interface IServicioService
    {
        Task<IEnumerable<Servicio>> ObtenerListaServicios();
        Task<Servicio> ObtenerServicioPorId(int id);
        Task GuardarServicio(Servicio servicio);
        Task EditarServicio(Servicio servicio);
        Task EliminarServicio(int id);
        Task<Servicio> NombreServicioExiste(string nombre);
    }
}
