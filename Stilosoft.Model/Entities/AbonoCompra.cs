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
        public int CantAbono { get; set; }
        [Column(TypeName = "Date")]
        public DateTime FechaPago { get; set; }
        
        [Required(ErrorMessage = "Las cuotas son obligatorias")]
        public int Cuotas { get; set; }
        public long ValorInicial { get; set; }
        public long ValorFinal { get; set; }
        public virtual Compra Compra { get; set; }
        public virtual DetalleCompra DetalleCompra { get; set; }

    }
}
