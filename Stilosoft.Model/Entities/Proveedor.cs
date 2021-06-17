﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Model.Entities
{
    public class Proveedor
    {
        [Key]
        public int ProveedorId { get; set; }

        [DisplayName("Nit")]
        [Required(ErrorMessage = "El nit es obligatorio")]
        [Column(TypeName = "nvarchar(25)")]
        public string Nit { get; set; }

        [DisplayName("Proveedor")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Column(TypeName = "nvarchar(50)")]
        public string Nombre { get; set; }

        [DisplayName("Direccion")]
        [Required(ErrorMessage = "La direccion es obligatoria")]
        [Column(TypeName = "nvarchar(50)")]
        public string Direccion { get; set; }

        [DisplayName("Telefono")]        
        [Column(TypeName = "nvarchar(10)")]
        public string Telefono { get; set; }

        [DisplayName("Contacto")]
        [Required(ErrorMessage = "El contacto es obligatorio")]
        [Column(TypeName = "nvarchar(25)")]
        public string Contacto { get; set; }

        [DisplayName("Telefono de contacto")]
        [Required(ErrorMessage = "El telefono de contacto es obligatorio")]
        [Column(TypeName = "nvarchar(10)")]
        public string TelefonoContacto { get; set; }

        [Required]    
        public bool Estado { get; set; }
        
    }
}
