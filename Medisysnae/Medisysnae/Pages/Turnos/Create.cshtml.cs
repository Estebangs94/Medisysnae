using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medisysnae.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Medisysnae.Pages.Turnos
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Turno TurnoActual { get; set; }
        [BindProperty]
        public string EstadoActual { get; set; }

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
            CargarPacientes();
            CargarEstados();

            return Page();
        }

        private void CargarEstados()
        {
            List<string> Estados = new List<string>();
            Estados.Add(Turno.Accion.Bloquear.ToString());
            Estados.Add(Turno.Accion.Cancelar.ToString());
            Estados.Add(Turno.Accion.Reservar.ToString());

            EstadosList = new SelectList(Estados);
        }

        private void CargarPacientes()
        {
            Pacientes = _context.Paciente.OrderBy(i => i.ApellidoNombre)
                .ToList();
            PacientesList = new SelectList(Pacientes, "ID", "ApellidoNombre", null);
           
        }

        public async Task<IActionResult> OnPostAsync()
        {


            return RedirectToPage("./Index");
        }
    }
}