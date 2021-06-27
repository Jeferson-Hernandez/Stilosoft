using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.ViewModels.Estilistas
{
    public class EstilistaViewModel
    {
        public int EstilistaId { get; set; }
        [DisplayName("Estilista")]
        [Required(ErrorMessage = "El nombre es obligatorio")]       
        public string Nombre { get; set; }
        [DisplayName("Fecha de nacimiento")]
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]        
        public DateTime FechaNacimiento { get; set; }
        [DisplayName("Cédula")]
        [Required(ErrorMessage = "La Cédula es obligatoria")]        
        public string Cedula { get; set; }
        [DisplayName("Celular")]
        [Required(ErrorMessage = "El celular es obligatorio")]        
        public string Celular { get; set; }
        public bool Estado { get; set; }

    }
}
