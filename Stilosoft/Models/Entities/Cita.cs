using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Models.Entities
{
    public class Cita
    {
        [Key]
        public int CitaId { get; set; } 
        [Required]
        public int UsuarioId { get; set; }
        [DisplayName("Fecha")]
        [Required(ErrorMessage = "La fecha es obligatoria")]        
        public DateTime Fecha { get; set; }
        [Required]
        public long Total { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        [Required]
        public string Estado { get; set; }
    }
}
