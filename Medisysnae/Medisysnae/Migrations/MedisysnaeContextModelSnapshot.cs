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

            modelBuilder.Entity("Medisysnae.Models.Antecedente", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<bool>("EsListaOpciones");

                    b.Property<bool>("EsTextArea");

                    b.Property<bool>("EsTitulo");

                    b.Property<bool>("EstaActivo");

                    b.Property<int>("Orden");

                    b.HasKey("ID");

                    b.ToTable("Antecedente");
                });

            modelBuilder.Entity("Medisysnae.Models.Antecedentespaciente", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AntecedenteID");

                    b.Property<string>("MedicoNombreUsuario");

                    b.Property<int?>("PacienteID");

                    b.Property<string>("ValorString");

                    b.HasKey("ID");

                    b.HasIndex("AntecedenteID");

                    b.HasIndex("MedicoNombreUsuario");

                    b.HasIndex("PacienteID");

                    b.ToTable("AntecedentePaciente");
                });

            modelBuilder.Entity("Medisysnae.Models.Atencion", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comentario");

                    b.Property<string>("Descripcion");

                    b.Property<string>("Diagnostico");

                    b.Property<bool>("EstaEliminada");

                    b.Property<DateTime>("FechaHora");

                    b.Property<string>("Medicacion");

                    b.Property<string>("MedicoNombreUsuario");

                    b.Property<int?>("PacienteID");

                    b.Property<string>("Titulo");

                    b.Property<int>("Tratamiento_ID");

                    b.HasKey("ID");

                    b.HasIndex("MedicoNombreUsuario");

                    b.HasIndex("PacienteID");

                    b.HasIndex("Tratamiento_ID");

                    b.ToTable("Atencion");
                });

            modelBuilder.Entity("Medisysnae.Models.Especialidad", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<string>("ProfesionalNombreUsuario");

                    b.HasKey("ID");

                    b.HasIndex("ProfesionalNombreUsuario");

                    b.ToTable("Especialidad");
                });

            modelBuilder.Entity("Medisysnae.Models.Obrasocial", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre");

                    b.HasKey("ID");

                    b.ToTable("Obrasocial");
                });

            modelBuilder.Entity("Medisysnae.Models.Paciente", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .IsRequired();

                    b.Property<string>("Comentario");

                    b.Property<int>("Dni");

                    b.Property<string>("Domicilio");

                    b.Property<bool>("EstaActivo");

                    b.Property<DateTime?>("FechaCreacion");

                    b.Property<string>("Mail");

                    b.Property<string>("MedicoNombreUsuario");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<string>("NroAfiliado");

                    b.Property<int>("Obrasocial_ID");

                    b.Property<long>("Telefono");

                    b.HasKey("ID");

                    b.HasIndex("MedicoNombreUsuario");

                    b.HasIndex("Obrasocial_ID");

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("Medisysnae.Models.Profesional", b =>
                {
                    b.Property<string>("NombreUsuario")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apellido")
                        .IsRequired();

                    b.Property<string>("DNI")
                        .IsRequired();

                    b.Property<string>("Direccion");

                    b.Property<bool>("EsAdministrador");

                    b.Property<bool>("EstaHabilitado");

                    b.Property<string>("Matricula");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<long>("Telefono");

                    b.HasKey("NombreUsuario");

                    b.ToTable("Profesional");
                });

            modelBuilder.Entity("Medisysnae.Models.Tratamiento", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CodigoNM");

                    b.Property<string>("Descripcion");

                    b.Property<int?>("EspecialidadID");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("EspecialidadID");

                    b.ToTable("Tratamiento");
                });

            modelBuilder.Entity("Medisysnae.Models.Turno", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comentario");

                    b.Property<string>("EstadoString");

                    b.Property<DateTime>("FechaTurno");

                    b.Property<TimeSpan>("HoraComienzo");

                    b.Property<TimeSpan>("HoraFin");

                    b.Property<string>("LugarAtencion");

                    b.Property<string>("NombreUsuario");

                    b.Property<int>("Paciente_ID");

                    b.HasKey("ID");

                    b.HasIndex("Paciente_ID");

                    b.ToTable("Turno");
                });

            modelBuilder.Entity("Medisysnae.Models.Antecedentespaciente", b =>
                {
                    b.HasOne("Medisysnae.Models.Antecedente", "Antecedente")
                        .WithMany()
                        .HasForeignKey("AntecedenteID");

                    b.HasOne("Medisysnae.Models.Profesional", "Medico")
                        .WithMany()
                        .HasForeignKey("MedicoNombreUsuario");

                    b.HasOne("Medisysnae.Models.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("PacienteID");
                });

            modelBuilder.Entity("Medisysnae.Models.Atencion", b =>
                {
                    b.HasOne("Medisysnae.Models.Profesional", "Medico")
                        .WithMany()
                        .HasForeignKey("MedicoNombreUsuario");

                    b.HasOne("Medisysnae.Models.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("PacienteID");

                    b.HasOne("Medisysnae.Models.Tratamiento", "Tratamiento")
                        .WithMany()
                        .HasForeignKey("Tratamiento_ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Medisysnae.Models.Especialidad", b =>
                {
                    b.HasOne("Medisysnae.Models.Profesional")
                        .WithMany("Especialidades")
                        .HasForeignKey("ProfesionalNombreUsuario");
                });

            modelBuilder.Entity("Medisysnae.Models.Paciente", b =>
                {
                    b.HasOne("Medisysnae.Models.Profesional", "Medico")
                        .WithMany()
                        .HasForeignKey("MedicoNombreUsuario");

                    b.HasOne("Medisysnae.Models.Obrasocial", "Obrasocial")
                        .WithMany()
                        .HasForeignKey("Obrasocial_ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Medisysnae.Models.Tratamiento", b =>
                {
                    b.HasOne("Medisysnae.Models.Especialidad")
                        .WithMany("Tratamientos")
                        .HasForeignKey("EspecialidadID");
                });

            modelBuilder.Entity("Medisysnae.Models.Turno", b =>
                {
                    b.HasOne("Medisysnae.Models.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("Paciente_ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
