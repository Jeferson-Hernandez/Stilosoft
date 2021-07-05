using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.ViewModels.Insumo
{
    public class InsumoViewModel
    {
        public int InsumoId { get; set; }
        [Required(ErrorMessage = "El nombre del insumo es obligatorio")]
        [Column(TypeName = "nvarchar(50)")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "La cantidad del insumo es obligatoria")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "La medida es obligatoria")]
        [Column(TypeName = "nvarchar(15)")]
        public string Medida { get; set; }

        
        public bool Estado { get; set; }

    }
}
