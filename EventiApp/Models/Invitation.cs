using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventiApp.Models
{
    public class Invitation
    {
        [Key]
        public int IdInvitation { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido {0}")]
        [Display(Name = "Evento")]
        public int IdEvent { get; set; }

        [Display(Name = "Empleado")]
        public int? IdEmployee { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [MaxLength(60, ErrorMessage = "El campo {0} Debe tener maximo {1} caracteres")]
        [Display(Name = "Correo Empleado")]
        [DataType(DataType.EmailAddress)]
        public string EmailEmployee { get; set; }

        [Display(Name = "Invitación aceptada")]
        public bool Accepted { get; set; }

        public virtual Event Event { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<MenuSelection> MenuSelections { get; set; }


    }
}