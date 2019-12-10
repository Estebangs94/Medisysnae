using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Medisysnae.Models
{
    public class Profesional
    {
        [Key]
        [Required(ErrorMessage ="El nombre de usuario es obligatorio")]
        [Display(Name = "Nombre de usuario")]
        [RegularExpression("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:.[a-zA-Z0-9-]+)*$", ErrorMessage = "El mail ingresado es inválido")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "El campo contraseña es obligatorio")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage ="La contraseña debe ser de al menos 8 caracteres")]
        public string Password { get; set; }

        [Display(Name ="Habilitado")]
        public bool EstaHabilitado { get; set; }
        [Display(Name ="Administrador")]
        public bool EsAdministrador { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo apellido es obligatorio")]
        public string Apellido { get; set; }
        public string Matricula { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo D.N.I es obligatorio")]
        [Display(Name = "D.N.I")]
        public string DNI { get; set; }

        public Int64 Telefono { get; set; } //Tiene que ser int64 sino no le dan los bits para representar el nro

        public IList<Especialidad> Especialidades { get; set; }

        [Display(Name ="Rol")]
        public string Rol { get
            {
                if (EsAdministrador == true)
                {
                    return "Administrador";
                }
                else return "Profesional";
            }
        }
        [Display(Name = "Habilitado")]
        public string Habilitado
        {
            get
            {
                if (EstaHabilitado == true)
                {
                    return "Si";
                }
                else return "No";
            }
        }
    }
}
