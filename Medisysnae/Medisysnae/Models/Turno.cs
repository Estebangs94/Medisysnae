using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medisysnae.Models
{
    public class Turno
    {
        public int ID { get; set; }

        public Profesional Medico { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime FechaHora { get; set; }
        public Tratamiento Tratamiento { get; set; }
        public enum Estado { Otorgado, Recepcionado, Cancelado};
        public DateTime? FechaHoraCancelacion { get; set; }

    }
}
