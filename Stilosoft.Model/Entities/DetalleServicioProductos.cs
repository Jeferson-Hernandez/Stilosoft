using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Model.Entities
{
    public class DetalleServicioProductos
    {
        [Key]
        public int DetalleServProductoId { get; set; }
        public int SolicitudServicioId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public long Precio { get; set; }
        public virtual SolicitudServicio SolicitudServicio { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
