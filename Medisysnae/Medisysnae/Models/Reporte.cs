using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medisysnae.Models
{
    public class Reporte
    {
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public int ObraSocialId { get; set; }
        public int TratamientoId { get; set; }
        public int PacienteId { get; set; }
        public string Estado { get; set; }
        public string LugarAtencion { get; set; }
    }
}
