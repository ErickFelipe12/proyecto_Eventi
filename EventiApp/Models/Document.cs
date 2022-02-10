using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventiApp.Models
{
    public class Document
    {
        [Key]
        [Display(Name = "Id Tipo de Documento")]
        public int IdDocumentType { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido {0}")]
        [StringLength(30, ErrorMessage = "El campo {0} Puede tener maximo {1} y minimo {2} caracteres", MinimumLength = 3)]
        [Display(Name = "Tipo de Documento")]
        public string DocumentType { get; set; }

       public virtual ICollection<User> User { get; set; }

    }
}