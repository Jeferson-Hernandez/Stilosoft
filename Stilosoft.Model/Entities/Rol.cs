using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Model.Entities
{
    public class Rol
    {
        [Key]
        public int RolId { get; set; }
        [DisplayName("Rol")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Column(TypeName = "nvarchar(15)")]
        public string NombreRol { get; set; }
    }
}
