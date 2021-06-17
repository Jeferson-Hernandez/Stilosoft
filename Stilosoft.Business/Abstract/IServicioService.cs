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
    }
}
