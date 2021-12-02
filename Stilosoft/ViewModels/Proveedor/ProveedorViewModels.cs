﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.ViewModels.Proveedor
{
    public class ProveedorViewModels
    {
        public int ProveedorId { get; set; }

        [DisplayName("Nit")]
        [Required(ErrorMessage = "El nit es obligatorio")]
        [StringLength(25, ErrorMessage = "Máximo 25 caracteres")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Ingrese valores numéricos")]
        public string Nit { get; set; }

        [DisplayName("Proveedor")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Ingrese caracteres")]
        public string Nombre { get; set; }

        [DisplayName("Direccion")]
        [Required(ErrorMessage = "La direccion es obligatoria")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Direccion { get; set; }

        [DisplayName("Telefono")]
        [StringLength(10, ErrorMessage = "Máximo 10 caracteres")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Ingrese valores numéricos")]
        public string Telefono { get; set; }

        [DisplayName("Contacto")]
        [Required(ErrorMessage = "El contacto es obligatorio")]
        [StringLength(25, ErrorMessage = "Máximo 25 caracteres")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Ingrese caracteres")]
        public string Contacto { get; set; }
        public bool Estado { get; set; }
    }
}
