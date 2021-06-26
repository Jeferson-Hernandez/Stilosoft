using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.ViewModels.Usuarios
{
    public class CrearUsuarioViewModel
    {        
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; }
        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }
        [Required(ErrorMessage = "El rol es obligatorio")]
        public string Rol { get; set; }
    }
}
