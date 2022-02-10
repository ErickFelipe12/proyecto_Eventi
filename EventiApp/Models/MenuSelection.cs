using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventiApp.Models
{
    public class MenuSelection
    {
        [Key]
        public int IdMenuSelection { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido {0}")]
        [Display(Name = "Invitación")]
        public int IdInvitation { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido {0}")]
        [Display(Name = "Evento")]
        public int IdMenu { get; set; }

        public virtual Invitation Invitation { get; set; }
        public virtual Menu Menu { get; set; }
    }
}