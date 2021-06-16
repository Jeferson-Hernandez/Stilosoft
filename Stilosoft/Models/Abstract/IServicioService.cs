using Stilosoft.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Models.Abstract
{
    public interface IServicioService
    {
        Task<IEnumerable<Servicio>> ObtenerListaServicios();
    }
}
