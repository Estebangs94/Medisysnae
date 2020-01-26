using System;
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
    public class IndexModel : PageModel
    {

        private readonly Medisysnae.Data.MedisysnaeContext _context;

        [BindProperty]
        public Profesional UsuarioActual { get; set; }
        
        [BindProperty]
        public DateTime DiaActual { get; set; }
        
        [BindProperty]
        public int Contador { get; set; }

        [BindProperty]
        public List<Turno> Turnos { get; set; }
        [BindProperty]
        public List<int> Numeros { get; set; }

        public SelectList PacienteList { get; set; }
        public List<Paciente> Pacientes { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Contador = 0;
            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");

            if (NombreUsuarioActual == null)
            {
                //caduco la sesion
                Page().StatusCode = 401;
                return Page();
            }

            UsuarioActual = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuarioActual);
            DiaActual = DateTime.Now.Date;

            this.CargarPacientes();

            return Page();
        }

        public IndexModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Contador = 0;

            return Page();
        }

        public async Task<IActionResult> OnPostChangeDate(DateTime date)
        {
            Contador = 0;
             
            DiaActual = date;

            //realizar busqueda de turnos del dia

            this.CargarPacientes();

            return Page();
        }


        private void CargarPacientes()
        {
            Pacientes = _context.Paciente.OrderBy(i => i.ApellidoNombre)
                .ToList();
            PacienteList = new SelectList(Pacientes, "ID", "ApellidoNombre", null);
        }

    }
}