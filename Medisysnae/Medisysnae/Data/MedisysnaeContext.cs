﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Medisysnae.Models;

namespace Medisysnae.Data
{
    public class MedisysnaeContext : DbContext
    {
        public MedisysnaeContext (DbContextOptions<MedisysnaeContext> options)
            : base(options)
        {
        }

        public DbSet<Medisysnae.Models.Profesional> Profesional { get; set; }
        public DbSet<Medisysnae.Models.Paciente> Paciente { get; set; }
        public DbSet<Medisysnae.Models.Atencion> Atencion { get; set; }
        public DbSet<Medisysnae.Models.Turno> Turno { get; set; }
        public DbSet<Medisysnae.Models.Obrasocial> Obrasocial { get; set; }
        public DbSet<Medisysnae.Models.Antecedente> Antecedente { get; set; }
        public DbSet<Medisysnae.Models.Antecedentespaciente> AntecedentePaciente { get; set; }
        public DbSet<Medisysnae.Models.Tratamiento> Tratamiento { get; set; }
        public DbSet<LugaresAtencion> LugaresAtencion { get; set; }
    }
}
