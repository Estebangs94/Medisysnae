﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medisysnae.Models
{
    public class Paciente
    {
        public int ID { get; set; }
        //Medico asociado
        public Profesional Medico { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [Display(Name ="DNI")]
        public int Dni { get; set; }

        //Solucion adoptada por problema con Entity Framework persistiendo entidad hija además de la padre 
        #region Foreing Key Obra Social
        //https://stackoverflow.com/questions/25441027/how-do-i-stop-entity-framework-from-trying-to-save-insert-child-objects
        [Display(Name = "Obra social")]
        [ForeignKey("Obrasocial_ID")]
        public Obrasocial Obrasocial { get; set; }
        public int Obrasocial_ID { get; set; }
        #endregion

        public string Domicilio { get; set; }
        public Int64 Telefono { get; set; }
        [Display(Name ="Nro afiliado")]
        public string NroAfiliado { get; set; }
        public string Comentario { get; set; }

        [RegularExpression("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:.[a-zA-Z0-9-]+)*$", ErrorMessage = "El mail ingresado es inválido")]
        public string Mail { get; set; }
        [Display()]
        public bool EstaActivo { get; set; }

        public string ApellidoNombre
        {
            get
            {
                return Apellido + ", " + Nombre;
            }
        }

    }
}
