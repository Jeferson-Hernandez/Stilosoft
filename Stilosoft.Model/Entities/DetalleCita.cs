using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Model.Entities
{
    public class DetalleCita
    {
        [Key]
        public int DetalleCitaId { get; set; }
        public int CitaId { get; set; }
        public int ServicioId { get; set; }
        public int EstilistaId { get; set; }
        public virtual Cita Cita { get; set; }
        public virtual Servicio Servicio { get; set; }
        public virtual Estilista Estilista { get; set; }
    }
}
