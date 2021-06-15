﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Models.Entities
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Column(TypeName = "nvarchar(50)")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "La categoria es obligatoria")]
        [Column(TypeName = "nvarchar(50)")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        public long Precio { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string RutaImagen { get; set; }
    }
}
