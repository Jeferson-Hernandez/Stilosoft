using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.ViewModels.SolicitudServicio
{
    public class SolicitudServicioViewModel
    {   [Required(ErrorMessage = "El cliente es obligatorio")]
        public string ClienteId { get; set; }
        [Required(ErrorMessage = "La fecha y hora son obligatorias")]
        public DateTime FechaHora { get; set; }
        public long TotalServicios { get; set; }
        public long TotalProductos { get; set; }
        public List<Servicio> Servicios { get; set; }
        public List<Producto> Productos { get; set; }
        public List<SolicitudServicios> ServiciosSolicitud { get; set; }
        public List<SolicitudProductos> ProductosSolicutud { get; set; }
    }
    public class SolicitudServicios
    {
        public int ServicioId { get; set; }
        public int Duracion { get; set; }
        public int EstilistaId { get; set; }
        public long Precio { get; set; }
    }
    public class SolicitudProductos
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public long Precio { get; set; }
    }
}
