using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Medisysnae.Models
{
    public class Paciente
    {
        public int ID { get; set; }
        public Profesional Medico { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public Obrasocial ObraSocial { get; set; }
        public string Domicilio { get; set; }
        public int Telefono { get; set; }
        public string NroAfiliado { get; set; }
        public string Comentario { get; set; }

        [RegularExpression("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:.[a-zA-Z0-9-]+)*$", ErrorMessage = "El mail ingresado es inválido")]
        public string Mail { get; set; }
        public bool EstaActivo { get; set; }
    }
}
