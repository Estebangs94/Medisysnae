using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Medisysnae.Models
{
    public class Usuario
    {
        public int ID { get; set; }

        [Required]
        [Display(Name ="Nombre de usuario")]
        public string NombreUsuario { get; set; }

        [MinLength(8,ErrorMessage ="La contrasena tiene que ser al menos 8 caracteres")]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }

      
        public int CodMedico { get; set; }
        public bool EstaHabilitado { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Fecha de creación")]
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }
    }
}
