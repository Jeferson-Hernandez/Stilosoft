using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Model.Entities
{
    public class Cita
    {
        [Key]
        public int CitaId { get; set; }
        [DisplayName("Cliente")]
        [Required]
        public int ClienteId { get; set; }        
        [Required(ErrorMessage = "La fecha es obligatoria")]
        [Column(TypeName = "Date")]
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "La hora es obligatoria")]
        [Column(TypeName = "nvarchar(20)")]
        public string Hora { get; set; }
        [Required]
        public long Total { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        [Required]
        public string Estado { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual List<DetalleCita> DetalleCitas { get; set; }
    }
}
