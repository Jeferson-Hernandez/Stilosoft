using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Business.Dtos
{
    public class CitaDetalleDto
    {
        [DisplayName("Cliente")]
        [Required(ErrorMessage = "El cliente es obligatorio")]
        public string ClienteId { get; set; }
        public List<CitaServiciosDto> Servicios { get; set; }

        [DisplayName("Fecha y hora")]
        [Required(ErrorMessage = "La fecha y hora es requerida")]
        public DateTime FechaHora { get; set; }
    }
    public class CitaServiciosDto
    {
        public int ServicioId { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public long Costo { get; set; }
        public bool Seleccionado { get; set; }
    }
}
