using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medisysnae.Models
{
    public class Atencion
    {
        public int ID { get; set; }

        public Profesional Medico { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime FechaHora { get; set; }
        public Tratamiento Tratamiento { get; set; }

        //Campo libre para el profesional
        public string Descripcion { get; set; }

        //Si es true no se muesta en la HC
        public bool EstaEliminada { get; set; } 
    }
}
