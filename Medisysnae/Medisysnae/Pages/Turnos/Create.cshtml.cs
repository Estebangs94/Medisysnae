﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Medisysnae.Data;
using Medisysnae.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Medisysnae.Pages.Turnos
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Turno TurnoActual { get; set; }
        [BindProperty]
        public string EstadoActual { get; set; }
        [BindProperty]
        public DateTime DiaActual { get; set; }

        public Profesional UsuarioActual { get; set; }

        public SelectList EstadosList { get; set; }
        public SelectList PacientesList { get; set; }
        public List<Paciente> Pacientes { get; set; }

        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public CreateModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");
            UsuarioActual = _context.Profesional.FirstOrDefault(m => m.NombreUsuario == NombreUsuarioActual);

            CargarPacientes();
            CargarEstados();

            TurnoActual = new Turno();
            TurnoActual.FechaTurno = DateTime.Now.Date;

            return Page();
        }

        private void CargarEstados()
        {
            List<string> Estados = new List<string>();
            Estados.Add(Turno.Accion.Bloquear.ToString());

            EstadosList = new SelectList(Estados);
        }

        private void CargarPacientes()
        {
            Pacientes = _context.Paciente.OrderBy(i => i.ApellidoNombre)
                .Include(m => m.Medico)
                .Where(p => p.Medico.NombreUsuario == UsuarioActual.NombreUsuario)
                .ToList();
            PacientesList = new SelectList(Pacientes, "ID", "ApellidoNombre", null);

        }

        public async Task<IActionResult> OnPostAsync()
        {
            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");
            UsuarioActual = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuarioActual);

            SetState();

            TurnoActual.Paciente_ID = TurnoActual.Paciente.ID;
            TurnoActual.Paciente = null;
            TurnoActual.NombreUsuario = UsuarioActual.NombreUsuario;

            List<Turno> turnosDiaProfesional = await _context.Turno.ToListAsync();
            turnosDiaProfesional = turnosDiaProfesional.Where(t => t.NombreUsuario == UsuarioActual.NombreUsuario).ToList();
            turnosDiaProfesional = turnosDiaProfesional.Where(t => t.FechaTurno.Date == TurnoActual.FechaTurno.Date).ToList();

            if (!Validar(turnosDiaProfesional))
            {
                CargarPacientes();
                CargarEstados();
                return Page();
            }

            _context.Turno.Add(TurnoActual);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool Validar(List<Turno> turnosDiaProfesional)
        {
            ModelState.Remove("TurnoActual.Paciente.Id");

            if (TurnoActual.Paciente_ID == 0)
            {
                ModelState.AddModelError(string.Empty, "Para otorgar un turno debe ingresar un paciente");
                return false;
            }
        

            if (turnosDiaProfesional.Count > 0)
            {
                foreach (var t in turnosDiaProfesional)
                {
                    if (TurnoActual.HoraComienzo > t.HoraComienzo && TurnoActual.HoraComienzo<t.HoraFin)
                    {
                        ModelState.AddModelError(string.Empty, $"El turno que ha intentado ingresar se superpone con el turno que comienza a las: {t.HoraComienzo.ToString()}" +
                            $" y finaliza a las {t.HoraFin.ToString()} horas."); //forma de mostrar error al usuario
                        return false;
                    }

                    if (TurnoActual.HoraFin > t.HoraComienzo && TurnoActual.HoraFin<t.HoraFin)
                    {
                        ModelState.AddModelError(string.Empty, $"El turno que ha intentado ingresar se superpone con el turno que comienza a las: {t.HoraComienzo.ToString()}" +
                            $" y finaliza a las {t.HoraFin.ToString()} horas."); //forma de mostrar error al usuario
                        return false;
                    }

                    if (TurnoActual.HoraComienzo <= t.HoraComienzo && TurnoActual.HoraFin > t.HoraComienzo)
                    {
                        ModelState.AddModelError(string.Empty, $"El turno que ha intentado ingresar se superpone con el turno que comienza a las: {t.HoraComienzo.ToString()}" +
                            $" y finaliza a las {t.HoraFin.ToString()} horas."); //forma de mostrar error al usuario
                        return false;
                    }
                }        
            }
            return true;
        }

        private void SetState()
{
    if (TurnoActual.EstadoString == null)
    {
        TurnoActual.EstadoString = Turno.Estado.Otorgado.ToString();
    }

    if (TurnoActual.EstadoString == Turno.Accion.Otorgar.ToString())
    {
        TurnoActual.EstadoString = Turno.Estado.Otorgado.ToString();
    }

    if (TurnoActual.EstadoString == Turno.Accion.Bloquear.ToString())
    {
        TurnoActual.EstadoString = Turno.Estado.Bloqueado.ToString();
    }

    if (TurnoActual.EstadoString == Turno.Accion.Cancelar.ToString())
    {
        TurnoActual.EstadoString = Turno.Estado.Cancelado.ToString();
    }

    if (TurnoActual.EstadoString == Turno.Accion.Reservar.ToString())
    {
        TurnoActual.EstadoString = Turno.Estado.Reservado.ToString();
    }
}
    }
}