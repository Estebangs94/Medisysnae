using System;
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

        public DbSet<Medisysnae.Models.Usuario> Usuario { get; set; }
    }
}
