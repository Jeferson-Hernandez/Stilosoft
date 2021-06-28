using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Model.Entities
{
    public class Estilista
    {
        [Key]
        public int EstilistaId { get; set; }
        [DisplayName("Estilista")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Column(TypeName = "nvarchar(50)")]
        public string Nombre { get; set; }
        [DisplayName("Edad")]
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [Column(TypeName = "Date")]
        public DateTime FechaNacimiento { get; set; }
        [DisplayName("Cédula")]
        [Required(ErrorMessage = "La Cédula es obligatoria")]
        [Column(TypeName = "nvarchar(15)")]
        public string Cedula { get; set; }
        [DisplayName("Celular")]
        [Required(ErrorMessage = "El celular es obligatorio")]
        [Column(TypeName = "nvarchar(15)")]
        public string Celular { get; set; }
        public bool Estado { get; set; }
        public virtual List<DetalleCita> DetalleCitas { get; set; }
        public virtual List<DetalleServicioServicios> DetalleServicioServicios { get; set; }
    }
}
