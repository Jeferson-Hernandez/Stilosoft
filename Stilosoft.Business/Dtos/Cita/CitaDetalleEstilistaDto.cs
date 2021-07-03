using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Business.Dtos.Cita
{
    public class CitaDetalleEstilistaDto
    {
        [Required]
        public int DetalleCitaId { get; set; }
        [Required]
        public int CitaId { get; set; }
        [Required]
        public int ServicioId { get; set; }
        [Required(ErrorMessage = "El estilista es obligatorio")]
        public int EstilistaId { get; set; }
    }
}
