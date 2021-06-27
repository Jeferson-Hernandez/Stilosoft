using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Model.Entities
{
    public class Servicio
    {
        [Key]
        public int ServicioId { get; set; }
        [DisplayName("Servicio")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Column(TypeName = "nvarchar(50)")]
        public string Nombre { get; set; }
        [DisplayName("Duración")]
        [Required(ErrorMessage = "La duración es obligatoria")]
        public int Duracion { get; set; }
        [Required(ErrorMessage = "El costo es obligatorio")]
        public long Costo { get; set; }
        [DisplayName("Categoría")]
        [Required(ErrorMessage = "La categoría es obligatoria")]
        [Column(TypeName = "nvarchar(50)")]
        public string Categoria { get; set; }
        public bool Estado { get; set; }
        public virtual List<DetalleCita> DetalleCitas { get; set; }
        public virtual List<DetalleServicioServicios> DetalleServicioServicios { get; set; }
        public virtual List<DetalleServicioInsumo> DetalleServicioInsumos { get; set; }
    }
}
