using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Business.Abstract
{
    public interface IEstilistaService
    {
        Task<IEnumerable<Estilista>> ObtenerListaEstilistas();
        Task<IEnumerable<Estilista>> CedulaEstilistaExisteEditar(string cedula);
        Task<Estilista> CedulaEstilistaExiste(string cedula);
        Task EliminarEstilista(int id);
        Task EditarEstilista(Estilista estilista);
        Task GuardarEstilista(Estilista estilista);
        Task<Estilista> ObtenerEstilistaPorId(int id);
        Task<IEnumerable<Estilista>> ObtenerListaEstilistasEstado();
    }
}
