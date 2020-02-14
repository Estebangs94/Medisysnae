using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medisysnae.Models
{
    public class LugaresAtencion
    {
        public int ID { get; set; }
        public string Lugar { get; set; }       
        public string UsuarioProfesional { get; set; }
        public bool EstaActivo { get; set; }
    }
}
