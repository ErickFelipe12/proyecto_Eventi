using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventiApp.Models
{
    public class Employee
    {
        [Key]
        public int IdEmployee { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [Display(Name = "Usuario")]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [MaxLength(60, ErrorMessage = "El campo {0} Debe tener maximo {1} caracteres")]
        [Display(Name = "Area O cargo")]
        public string Position { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }

    }
}