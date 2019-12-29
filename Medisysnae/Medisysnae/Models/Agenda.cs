using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medisysnae.Models
{
    public class Agenda
    {
        public enum Dias { Lunes,Martes,Miercoles,Jueves,Viernes,Sabados}
        public TimeSpan HoraInicio = new TimeSpan(08, 00, 00);
        public TimeSpan HoraFin = new TimeSpan(20, 00, 00);
        public TimeSpan Duracion = new TimeSpan(00, 20, 00);
    }
}
