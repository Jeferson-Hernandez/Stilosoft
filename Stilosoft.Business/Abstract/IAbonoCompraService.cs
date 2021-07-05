using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Business.Abstract
{
    public interface IAbonoCompraService
    {
        Task<IEnumerable<AbonoCompra>> ObtenerListaAbonoCompra();
        Task GuardarAbonoCompra(AbonoCompra abonoCompra);
        Task<AbonoCompra> ObtenerAbonoCompraPorId(int id);
       // Task EditarProducto(AbonoCompra abonoCompra);
        Task EliminarAbonoCompra(int id);
    }
}
