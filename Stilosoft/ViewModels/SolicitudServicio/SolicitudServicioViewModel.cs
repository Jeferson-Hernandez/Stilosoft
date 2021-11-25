using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.ViewModels.SolicitudServicio
{
    public class SolicitudServicioViewModel
    {
        [Required]
        public string clienteId { get; set; }
        public List<Servicio> servicios { get; set; }
        public List<Producto> productos { get; set; }
    }
}
