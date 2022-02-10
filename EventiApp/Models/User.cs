using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventiApp.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [MaxLength(60, ErrorMessage = "El campo {0} Debe tener maximo {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [MaxLength(60, ErrorMessage = "El campo {0} Debe tener maximo {1} caracteres")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [MaxLength(40, ErrorMessage = "El campo {0} Debe tener maximo {1} caracteres")]
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [MaxLength(70, ErrorMessage = "El campo {0} Debe tener maximo {1} caracteres")]
        [Display(Name = "Correo electronico")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [MaxLength(30, ErrorMessage = "El campo {0} Debe tener maximo {1} caracteres")]
        [Display(Name = "Identificación")]
        public string Identification { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [Display(Name = "Tipo de Documento")]
        public int IdDocumentType { get; set; }

       public virtual Document Document { get; set; }

       public virtual ICollection<Employee> Employees { get; set; }

       public virtual ICollection<Client> Clients { get; set; }

    }

}