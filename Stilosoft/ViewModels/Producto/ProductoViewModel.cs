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
        [StringLength(35, ErrorMessage = "Máximo 35 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Ingrese valores numéricos")]
        [Range(1, 999999, ErrorMessage = "El rango máximo del precio es de 1.000.000")]
        public long Precio { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string Descripcion { get; set; }
        [Required]
        public bool Estado { get; set; }

        public IFormFile Imagen { get; set; }
        [DisplayName("Imagen")]
        public string RutaImagen { get; set; }
    }
}
