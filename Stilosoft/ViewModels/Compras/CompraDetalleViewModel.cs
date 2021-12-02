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
        [Required]
        [DisplayName("Compra")]
        public int CompraId { get; set; }
        [Required]
        [DisplayName("Producto")]
        public int ProductoId { get; set; }
        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, 999, ErrorMessage = "El rango de la cantidad es de 1 a 999")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Ingrese valores numéricos")]
        public int Cantidad { get; set; }
        [Required(ErrorMessage = "El costo es obligatorio")]
        [Range(1, 999999, ErrorMessage = "El rango del precio es de 1 a 999.999")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Ingrese valores numéricos")]
        public long Costo { get; set; }
        [DisplayName("Subtotal")]
        [Required]
        public long SubTotal { get; set; }
        [DisplayName("% IVA")]
        [Range(1, 75, ErrorMessage = "El rango del IVA es de 1 a 75")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Ingrese valores numéricos")]
        [Required]
        public int Iva { get; set; }
        [Required]
        public long Total { get; set; }
    }
}
