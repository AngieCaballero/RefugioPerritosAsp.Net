using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Practica1.Models {
    public class Interesados {
        public int id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Correo")]
        public string Correo { get; set; }

        [Required]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Required]
        [Display(Name = "Salario")]
        public float Salario { get; set; }

        [Required]
        [Display(Name = "Patio")]
        public string Patio { get; set; }

        [Required]
        [Display(Name = "Texto")]
        public string Texto { get; set; }

        public int PerritosId { get; set; }
        public Perritos Perritos { get; set; }
    }
}