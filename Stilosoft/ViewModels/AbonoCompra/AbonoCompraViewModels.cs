using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.ViewModels.AbonoCompra
{
    public class AbonoCompraViewModels
    {
        public int AbonoCompraId { get; set; }
        public int CompraId { get; set; }
        [DisplayName("Abono")]
        [Required(ErrorMessage = "El abono es obligatorio")]
        public int CantAbono { get; set; }
        [Column(TypeName = "Date")]
        [DisplayName("Fecha de pago")]
        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime FechaPago { get; set; }
        public int Cuotas { get; set; }
        public int CuotasPagadas { get; set; }
        [DisplayName("Valor final")]
        public long PrecioTotal { get; set; }
        public long MontoAbonado { get; set; }
    }
}
