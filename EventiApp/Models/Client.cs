using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventiApp.Models
{
    public class Client
    {
        [Key]
        public int IdClient { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [Display(Name = "Usuario")]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [MaxLength(60, ErrorMessage = "El campo {0} Debe tener maximo {1} caracteres")]
        [Display(Name = "Direccion Empresa")]
        public string Address { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [Display(Name = "Numero de empleados")]
        public int NumberEmployees { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Event> Events { get; set; }


    }
}