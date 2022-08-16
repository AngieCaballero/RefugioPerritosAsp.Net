using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Practica1.Models {
    public class Perritos {
        public int id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Raza")]
        public string Raza { get; set; }

        [Required]
        [Display(Name = "Color")]
        public string Color { get; set; }

        [Required]
        [Display(Name = "Edad")]
        public string Edad { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        [Required]
        [Display(Name = "Salud")]
        public string Salud { get; set; }

        [Required]
        [Display(Name = "Personalidad")]
        public string Personalidad { get; set; }

        [Required]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "Foto")]
        public string Foto { get; set; }

        [Required]
        [Display(Name = "Tema")]
        public string Tema { get; set; }

        public List<Interesados> Interesados { get; set; }

        public Perritos() {
            Interesados = new List<Interesados>();
        }

    }
}