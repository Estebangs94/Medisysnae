using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Medisysnae.Models
{
    public class Tratamiento
    {
        public int ID { get; set; }

        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? CodigoNM { get; set; }
    }
}
