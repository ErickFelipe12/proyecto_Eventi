using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventiApp.Models
{
    public class Menu
    {
        [Key]
        public int IdMenu { get; set; }

        public int IdEvent { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [MaxLength(500, ErrorMessage = "El campo {0} Debe tener maximo {1} caracteres")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }


        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} Debe tener maximo {1} caracteres")]
        [Display(Name = "Tipo de menú")]
        public string Type { get; set; }

        public virtual Event Event { get; set; }

        public virtual ICollection<MenuSelection> MenuSelections { get; set; }

    }
}