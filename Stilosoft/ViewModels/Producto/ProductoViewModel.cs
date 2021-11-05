using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.ViewModels
{
    public class ProductoViewModel
    {
        public int ProductoId { get; set; }

        [DisplayName("Insumo")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Column(TypeName = "nvarchar(50)")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        public long Precio { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; }

        public IFormFile Imagen { get; set; }
        [DisplayName("Imagen")]
        public string RutaImagen { get; set; }
    }
}
