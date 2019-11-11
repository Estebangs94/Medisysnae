using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medisysnae.Models
{
    public class Antecedente
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public bool EstaActivo { get; set; }
        public bool EsListaOpciones { get; set; }
        public int Orden { get; set; }
    }
}
