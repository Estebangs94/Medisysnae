using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medisysnae.Models
{
    public class Antecedentespaciente
    {
        //No tiene sentido hacer el alta de los valores de los antecedentes en la creacion del paciente, 
        //ya que el paciente llama al prof, toma sus datos perosnales y luego en la primer consulta
        //le hace una entrevista y carga sus antecedentes
        public int ID { get; set; }
        public Antecedente Antecedente { get; set; }
        public Paciente Paciente { get; set; }
        public Profesional Medico { get; set; }
        public string Valor { get; set; }
    

        //https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/crud?view=aspnetcore-2.1#overposting
    }
}
