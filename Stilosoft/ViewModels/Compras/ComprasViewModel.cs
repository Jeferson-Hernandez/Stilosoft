using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.ViewModels
{
    public class ComprasViewModel
    {
        public int CompraId { get; set; }
        [Required(ErrorMessage = "El proveedor es obligatorio")]
        [DisplayName("Proveedor")]
        public int ProveedorId { get; set; }
        [Required(ErrorMessage = "La fecha de la facutra es obligatoria")]
        [Column(TypeName = "Date")]
        [DisplayName("Fecha de factura")]
        public DateTime FechaFactura { get; set; }
        [Required(ErrorMessage = "El número de la factura es obligatorio")]
        [Column(TypeName = "nvarchar(20)")]
        [DisplayName("Número de factura")]
        [StringLength(15, ErrorMessage = "Máximo 15 caracteres")]
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
        [Range(1,30, ErrorMessage = "En rango de las cuotas es de 1 a 30")]
        public int Cuotas { get; set; }
        [DisplayName("Factura")]
        [Required(ErrorMessage = "La factura es obligatoria")]
        public IFormFile Imagen { get; set; }
        [DisplayName("Factura")]
        public string RutaImagen { get; set; }
    }
}
