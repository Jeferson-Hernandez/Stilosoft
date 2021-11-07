using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Business.Abstract
{
    public interface IDetalleCompraService
    {
        Task<IEnumerable<DetalleCompra>> ObtenerDetalleCompras();
        Task<IEnumerable<DetalleCompra>> ObtenerListaDetallePorId(int Id);
        Task<DetalleCompra> ObtenerDetalleCompraId(int Id);
        Task RegistrarDetalleCompra(DetalleCompra detalleCompra);
        Task EditarDetalle(DetalleCompra detalleCompra);
        Task EliminarDetalleCompra(int Id);
        Task<DetalleCompra> ProductoExiste(int compraId, int Producto);


    }
}
