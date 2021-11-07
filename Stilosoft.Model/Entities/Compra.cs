using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Required(ErrorMessage = "El proveedor es obligatorio")]
        [DisplayName("Proveedor")]
        public int ProveedorId { get; set; }
        [Required(ErrorMessage = "La fecha de la compra es obligatoria")]
        [Column(TypeName = "Date")]
        [DisplayName("Fecha de compra")]
        public DateTime FechaFactura { get; set; }
        [Required(ErrorMessage = "El número de la factura es obligatorio")]
        [Column(TypeName = "nvarchar(20)")]
        [DisplayName("Número de factura")]
        public string NoFactura { get; set; }
        [Required(ErrorMessage = "La forma de pago es obligatoria")]
        [Column(TypeName = "nvarchar(15)")]
        [DisplayName("Forma de pago")]
        public string FormaPago { get; set; }
        [Required(ErrorMessage = "La fecha de inicio del pago es obligatoria")]
        [Column(TypeName = "Date")]
        [DisplayName("Inicio de pago")]
        public DateTime FechaInicioPago { get; set; }
        [Required(ErrorMessage = "La fecha de registro es obligatoria")]
        [Column(TypeName = "Date")]
        [DisplayName("Fecha de registro")]
        public DateTime FechaRegistro { get; set; }
        [Required(ErrorMessage = "La periodicidad es obligatoria")]
        [Column(TypeName = "nvarchar(20)")]
        public string Periodicidad { get; set; }
        [Required(ErrorMessage = "Las cuotas son obligatorias")]
        public int Cuotas { get; set; }
        [DisplayName("Factura")]
        [Required(ErrorMessage = "La factura es obligatoria")]
        public string RutaImagen { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual List<DetalleCompra> DetalleCompras { get; set; }
    }
}
