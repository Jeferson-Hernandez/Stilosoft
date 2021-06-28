using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.ViewModels.Citas
{
    public class CitaViewModel
    {
        [DisplayName("Cliente")]
        public string ClienteId { get; set; }
        [DisplayName("Servicio 1")]
        [Required(ErrorMessage = "La cita debe tener al menos un servicio")]
        public int ServicioId1 { get; set; }
        [DisplayName("Servicio 2")]
        public int ServicioId2 { get; set; }
        [DisplayName("Servicio 3")]
        public int ServicioId3 { get; set; }
        [DisplayName("Servicio 4")]
        public int ServicioId4 { get; set; }
        [DisplayName("Servicio 5")]
        public int ServicioId5 { get; set; }
        [DisplayName("Fecha y hora")]
        [Required(ErrorMessage = "La fecha y hora es requerida")]
        public DateTime FechaHora { get; set; }

    }
}
