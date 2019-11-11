using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Medisysnae.Models
{
    public class Obrasocial
    {
        [Key]
        public int ID { get; set; }

        public string Nombre { get; set; }
    }
}
