using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Model.Entities
{
    public class Compra
    {
        [Key]
        public int CompraId { get; set; }
        public int ProveedorId { get; set; }
        public int Cantidad { get; set; }
        public long PrecioTotal { get; set; }
        [Column(TypeName = "Date")]
        public DateTime FechaFactura { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string NoFactura { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        public string FormaPago { get; set; }
        [Column(TypeName = "Date")]
        public DateTime FechaInicioPago { get; set; }
        [Column(TypeName = "Date")]
        public DateTime FechaRegistro { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Periodicidad { get; set; }
        public int Cuotas { get; set; }
        public string RutaImagen { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual List<DetalleCompra> DetalleCompras { get; set; }
    }
}
