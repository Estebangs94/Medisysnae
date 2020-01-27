using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medisysnae.Models
{
    public class Turno
    {
        public int ID { get; set; }
        public int CodPaciente { get; set; }
        public string Comentario { get; set; }
        public int CodMedico { get; set; }
        public string LugarAtencion { get; set; }

        public enum Estado { Otorgado, Reservado, Cancelado, Atendido, Bloqueado };
        public enum Accion { Otorgar, Reservar, Cancelar, Atender, Bloquear };

        public DateTime FechaTurno { get; set; }
        public TimeSpan HoraComienzo { get; set; }
        public TimeSpan HoraFin { get; set; }


    }
}
