using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medisysnae.Models
{
    public class Turno
    {
        public int ID { get; set; }

        public int CodMedico { get; set; }
        public int CodPaciente { get; set; }
        public DateTime FechaTurno { get; set; }
        public enum Estado { Otorgado, Reservado, Cancelado, Atendido, Bloqueado};
        public DateTime? HoraComienzo { get; set; }
        public DateTime? HoraFin { get; set; }
        public DateTime? FechaHoraCancelacion { get; set; }

    }
}
