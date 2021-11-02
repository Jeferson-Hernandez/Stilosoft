using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Model.Entities
{
    public class DetalleServicioServicios
    {
        [Key]
        public int ServicioServiciosId { get; set; }
        public int SolicitudServicioId { get; set; }
        public int ServicioId { get; set; }
        public int EstilistaId { get; set; }
        public long Precio { get; set; }
        public virtual SolicitudServicio SolicitudServicio { get; set; }
        public virtual Servicio Servicio { get; set; }
        public virtual Estilista Estilista { get; set; }
    }
}
