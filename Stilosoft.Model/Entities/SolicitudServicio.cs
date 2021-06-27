using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Model.Entities
{
    public class SolicitudServicio
    {
        [Key]
        public int SolicitudServicioId { get; set; }
        public int ClienteId { get; set; }
        
        [Column(TypeName = "Date")]
        public DateTime Fecha { get; set; }
        
        [Column(TypeName = "Time")]
        public DateTime Hora { get; set; }
        public long Total { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Estado { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual List<DetalleServicioServicios> DetalleServicioServicios { get; set; }
        public virtual List<DetalleServicioProductos> DetalleServicioProductos { get; set; }
    }
}
