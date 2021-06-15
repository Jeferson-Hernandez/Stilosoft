using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Models.Entities
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }        
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Column(TypeName = "nvarchar(50)")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio")]
        [Column(TypeName = "nvarchar(50)")]
        public string Apellido { get; set; }
        [DisplayName("Número")]
        [Required(ErrorMessage = "El número es obligatorio")]
        [Column(TypeName = "nvarchar(10)")]
        public string Numero { get; set; }
        [DisplayName("Cédula")]
        [Required(ErrorMessage = "La Cédula es obligatoria")]
        [Column(TypeName = "nvarchar(15)")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "El correo es obligatorio")]
        [Column(TypeName = "nvarchar(100)")]
        public string Correo { get; set; }
        [Required]
        public int RolId { get; set; }
        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }

        public bool Estado { get; set; }
    }
}
