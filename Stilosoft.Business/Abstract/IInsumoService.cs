using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Business.Abstract
{
    public interface IInsumoService
    {
        Task<IEnumerable<Insumo>> ObtenerListaInsumos();
        Task RegistrarInsumo(Insumo insumo);
        Task<Insumo> ObtenerInsumoPorId(int Id);
        Task EditarInsumo(Insumo insumo);
        Task EliminarInsumo(int Id);
        Task<Insumo> NombreInsumoExiste(string Nombre);
    }
}
