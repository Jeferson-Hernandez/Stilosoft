using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Business.Abstract
{
    public interface IComprasService
    {
        Task<IEnumerable<Compra>> ObtenerListaCompras();
        Task<IEnumerable<DetalleCompra>> ObtenerDetalleCompras();
        Task RegistrarCompra(Compra compra);
        Task RegistrarDetalleCompra(DetalleCompra detalleCompra);
        Task<Compra> ObtenerCompraPorId(int Id);
        Task<DetalleCompra> ObtenerDetalleCompraPorId(int Id);
        Task EditarDetalleCompra(DetalleCompra detalleCompra);
        Task EliminarCompra(int Id);
        Task EliminarDetalleCompra(int Id);
        Task<Compra> NoFacturaExiste(string NoFactura);
        Task<DetalleCompra> ProductoExiste(int Producto);
    }
}
