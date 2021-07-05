using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Business.Dtos.Cita
{
    public class CitaServiciosDto
    {
        public int ServicioId { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public long Costo { get; set; }
        public bool Seleccionado { get; set; }
    }
}
