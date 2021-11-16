using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Model.Entities
{
    public class AbonoCompra
    {
        [Key]
        public int AbonoCompraId { get; set; }
        [ForeignKey("Compra")]
        public int CompraId { get; set; }
        [DisplayName("Cantidad abonada")]
        public int CantAbono { get; set; }
        [Column(TypeName = "Date")]
        [DisplayName("Fecha de pago")]
        public DateTime FechaPago { get; set; }
        
        [Required(ErrorMessage = "Las cuotas son obligatorias")]
        public int Cuotas { get; set; }
        [DisplayName("Valor inicial")]
        public long ValorInicial { get; set; }
        [DisplayName("Valor final")]
        public long ValorFinal { get; set; }
        
        public virtual Compra Compra { get; set; }
        public virtual DetalleCompra DetalleCompra { get; set; }

    }
}
