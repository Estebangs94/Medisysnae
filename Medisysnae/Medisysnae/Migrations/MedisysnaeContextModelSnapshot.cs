﻿// <auto-generated />
using System;
using Medisysnae.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Medisysnae.Migrations
{
    [DbContext(typeof(MedisysnaeContext))]
    partial class MedisysnaeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Medisysnae.Models.Usuario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodMedico");

                    b.Property<string>("Contrasena");

                    b.Property<string>("Email");

                    b.Property<bool>("EstaHabilitado");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<string>("NombreUsuario");

                    b.HasKey("ID");

                    b.ToTable("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
