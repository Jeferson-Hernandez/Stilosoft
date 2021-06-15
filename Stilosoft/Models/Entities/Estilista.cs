using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Models.Entities
{
    public class Estilista
    {
        [Key]
        public int EstilistaId { get; set; }
        [DisplayName("Estilista")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Column(TypeName = "nvarchar(50)")]
        public int Nombre { get; set; }
        [DisplayName("Fecha de nacimiento")]
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime FechaNacimiento { get; set; }
        [DisplayName("Cédula")]
        [Required(ErrorMessage = "La Cédula es obligatoria")]
        [Column(TypeName = "nvarchar(15)")]
        public string cedula { get; set; }
        [DisplayName("Celular")]
        [Required(ErrorMessage = "El celular es obligatorio")]
        [Column(TypeName = "nvarchar(15)")]
        public string celular { get; set; }
    }
}
