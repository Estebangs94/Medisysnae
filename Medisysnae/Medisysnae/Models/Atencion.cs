using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medisysnae.Models
{
    public class Atencion
    {
        public int ID { get; set; }

        public Profesional Medico { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime FechaHora { get; set; }


        [Display(Name = "Tratamiento")]
        [ForeignKey("Tratamiento_ID")]
        public Tratamiento Tratamiento { get; set; }
        public int Tratamiento_ID { get; set; }


        public string Diagnostico { get; set; }

        //Resumen del día de la HC
        public string Titulo { get; set; }
        //Campo libre para el profesional
        public string Descripcion { get; set; } //es el campo indicaciones

        //Si es true no se muesta en la HC
        public bool EstaEliminada { get; set; } 

        public string Medicacion { get; set; }
        public string Comentario { get; set; }
    }
}
