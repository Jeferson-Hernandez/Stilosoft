using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.ViewModels.Compras
{
    public class CompraInsumosViewModel
    {
        public int compraId { get; set; }
        public List<ListaInsumos> CompraInsumos { get; set; }
    }

    public class ListaInsumos
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public long Costo { get; set; }
        public long SubTotal { get; set; }
        public int Iva { get; set; }
        public long Total { get; set; }
    }
}
