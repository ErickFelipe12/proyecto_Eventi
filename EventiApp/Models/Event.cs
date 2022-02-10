using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventiApp.Models
{
    public class Event
    {
        [Key]
        public int IdEvent  { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [Display(Name = "Cliente")]
        public int IdClient { get; set; }


        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} Debe tener maximo {1} caracteres")]
        [Display(Name = "Lugar")]
        public string Place { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [MaxLength(40, ErrorMessage = "El campo {0} Debe tener maximo {1} caracteres")]
        [Display(Name = "Tipo de evento")]
        public string Type { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [MaxLength(500, ErrorMessage = "El campo {0} Debe tener maximo {1} caracteres")]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [MaxLength(20, ErrorMessage = "El campo {0} Debe tener maximo {1} caracteres")]
        [Display(Name = "Hora")]
        public string Hour { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [MaxLength(30, ErrorMessage = "El campo {0} Debe tener maximo {1} caracteres")]
        [Display(Name = "Fecha")]
        public string Date { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }

       
        public virtual ICollection<Menu> Menu { get; set; }



    }
}