using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Model.Entities
{
    public class DetalleServicioInsumo
    {
        [Key]
        public int DetalleServInsumoId { get; set; }
        public int ServicioId { get; set; }
        public int InsumoId { get; set; }
        public int Cantidad { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Medida { get; set; }
        public long Precio { get; set; }
        public virtual Servicio Servicio { get; set; }
        public virtual Insumo Insumo { get; set; }
    }
}
