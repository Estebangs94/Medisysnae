using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Medisysnae.Models
{
    public class Turno
    {
        public int ID { get; set; }

        [ForeignKey("Paciente_ID")]
        public Paciente Paciente { get; set; }
        public int? Paciente_ID { get; set; }

        public string Comentario { get; set; }
        public string NombreUsuario { get; set; }
        public string LugarAtencion { get; set; }
        public string EstadoString { get; set; }

        public enum Estado { Otorgado, Reservado, Cancelado, Atendido, Bloqueado };
        public enum Accion { Otorgar, Reservar, Cancelar, Atender, Bloquear };

        public DateTime FechaTurno { get; set; }
        public string FechaTurnoString { get; set; }
        public TimeSpan HoraComienzo { get; set; }
        public TimeSpan HoraFin { get; set; }

        public bool EstaActivo { get; set; }

    }
}
