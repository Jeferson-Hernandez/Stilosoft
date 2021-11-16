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
        long ObtenerAbonoPorId(int Id);
        int ObtenerAbonoCuotaPorId(int Id);
        //int ObtenerCuotasPorId(int Id);
        long ObtenerMontoAbonadoPorId(int Id);
        long ObtenerTotalDetalleCompraPorId(int Id);
        Task<IEnumerable<AbonoCompra>> ObtenerListaAbonoPorId(int Id);
        Task<AbonoCompra> ObtenerAbonoCompraId(int Id);
        Task EliminarAbonoCompra(int Id);


    }
}
