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
    public class DetalleCompra
    {
        [Key]
        public int DetalleCompraId { get; set; }
        public int CompraId { get; set; }
        [DisplayName("Producto")]
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public long Costo { get; set; }
        [DisplayName("Subtotal")]
        public long SubTotal { get; set; }
        public int Iva { get; set; }
        public long Total { get; set; }
        public virtual Compra Compra { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
