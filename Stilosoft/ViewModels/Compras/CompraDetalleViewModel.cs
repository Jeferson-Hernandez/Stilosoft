using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.ViewModels.Compras
{
    public class CompraDetalleViewModel
    {
        public int DetalleCompraId { get; set; }
        public int CompraId { get; set; }
        public int ProductoId { get; set; }
        public int InsumoId { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required(ErrorMessage = "El insumo es obligatorio")]
        [DisplayName("Cantidad para insumos")]
        public int CantInsumo { get; set; }
        [Required(ErrorMessage = "El producto es obligatorio")]
        [DisplayName("Cantidad para productos")]
        public int CantProducto { get; set; }
        public long Costo { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Medida { get; set; }
        public long SubTotal { get; set; }
        [DisplayName("% IVA")]
        public int Iva { get; set; }
        public long Total { get; set; }
    }
}
