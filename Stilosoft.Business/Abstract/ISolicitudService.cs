using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Business.Abstract
{
    public interface ISolicitudService
    {
        Task<IEnumerable<SolicitudServicio>> ObtenerListaSolicitudes();
    }
}
