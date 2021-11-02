﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.ViewModels.Usuarios
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El Numero es obligatorio")]
        public string Numero { get; set; }
        [DisplayName("Documento")]
        [Required(ErrorMessage = "EL documento es obligatorio")]
        public string Documento { get; set; }
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; }
        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }
    }
}
