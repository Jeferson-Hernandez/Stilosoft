﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.ViewModels.Servicios
{
    public class ServicioViewModel
    {
        public int ServicioId { get; set; }
        [DisplayName("Servicio")]
        [Required(ErrorMessage = "El servicio es obligatorio")]
        [StringLength(25, ErrorMessage = "Máximo 25 caracteres")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Ingrese caracteres")]
        public string Nombre { get; set; }
        [DisplayName("Duración")]
        [Required(ErrorMessage = "La duración es obligatoria")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Ingrese valores numéricos")]
        public int Duracion { get; set; }
        [Required(ErrorMessage = "El costo es obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Ingrese valores numéricos")]
        public long Costo { get; set; }
        public bool Estado { get; set; }
    }
}
