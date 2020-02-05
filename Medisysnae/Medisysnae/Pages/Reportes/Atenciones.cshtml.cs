using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medisysnae.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Medisysnae.Pages.Reportes
{
    public class AtencionesModel : PageModel
    {
        [BindProperty]
        public Profesional UsuarioActual { get; set; }

        [BindProperty]
        public Reporte Reportes { get; set; }

        public IList<Obrasocial> ObraSocialesTodas { get; set; }
        public SelectList ObraSocialesList { get; set; }

        public SelectList PacientesList { get; set; }
        public List<Paciente> Pacientes { get; set; }

        public IList<Tratamiento> TratamientosTodos { get; set; }
        public SelectList TratamientosList { get; set; }

        private readonly Data.MedisysnaeContext _context;

        public AtencionesModel(Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");
            UsuarioActual = _context.Profesional.FirstOrDefault(m => m.NombreUsuario == NombreUsuarioActual);

            CargarTratamientos();
            CargarObraSociales();
            CargarPacientes();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");
            UsuarioActual = _context.Profesional.FirstOrDefault(m => m.NombreUsuario == NombreUsuarioActual);

            return Page();
        }

        private void CargarTratamientos()
        {
            TratamientosTodos = _context.Tratamiento.ToList();
            TratamientosList = new SelectList(TratamientosTodos, "ID", "Nombre", null);
        }

        private void CargarObraSociales()
        {
            ObraSocialesTodas = _context.Obrasocial.ToList();
            ObraSocialesList = new SelectList(ObraSocialesTodas, "ID", "Nombre", null);
        }


        private void CargarPacientes()
        {
            Pacientes = _context.Paciente.OrderBy(i => i.ApellidoNombre)
                .Include(m => m.Medico)
                .Where(p => p.Medico.NombreUsuario == UsuarioActual.NombreUsuario)
                .ToList();
            PacientesList = new SelectList(Pacientes, "ID", "ApellidoNombre", null);
        }
    }
}