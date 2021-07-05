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
        public List<CitaDetalleAsignarEstilistaDto> Estilistas { get; set; }
    }
    public class CitaDetalleAsignarEstilistaDto
    {
        [Required]
        public int DetalleCitaId { get; set; }
        [Required]
        public int CitaId { get; set; }
        [Required]
        public int ServicioId { get; set; }
        [Required(ErrorMessage = "El estilista es obligatorio")]
        public int? EstilistaId { get; set; }
        public string NombreServicio { get; set; }
        public long CostoServicio { get; set; }
    }
}
