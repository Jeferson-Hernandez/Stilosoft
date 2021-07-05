﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stilosoft.Model.Entities
{
    public class AbonoCompra
    {
        [Key]
        public int AbonoCompraId { get; set; }
        public int CantAbono { get; set; }
        [Column(TypeName = "Date")]
        public DateTime FechaPago { get; set; }
        public int Cuotas { get; set; }
        public int CuotasPagadas { get; set; }
        public long MontoAbonado { get; set; }
        public long PrecioTotal { get; set; }
        public int CompraId { get; set; }
        public virtual Compra Compra { get; set; }

    }
}
