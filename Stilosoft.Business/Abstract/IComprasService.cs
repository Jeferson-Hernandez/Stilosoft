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
        
        Task RegistrarCompra(Compra compra);
        
        Task<Compra> ObtenerCompraPorId(int Id);
       
        Task EliminarCompra(int Id);
        Task<Compra> NoFacturaExiste(string NoFactura);

        // Detalle

    }
}
