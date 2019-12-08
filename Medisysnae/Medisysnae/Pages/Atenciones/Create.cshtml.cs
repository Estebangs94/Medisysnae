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

namespace Medisysnae.Pages.Atenciones
{
    public class CreateModel : PageModel
    {
        public Profesional UsuarioActual { get; set; }
        public Paciente Paciente { get; set; }

        [BindProperty]
        public string DescripcionAtencion { get; set; }

        [BindProperty]
        public string TituloAtencion { get; set; }

        [BindProperty]
        public DateTime FechaAtencion { get; set; }

        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public CreateModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Paciente = await _context.Paciente.FirstOrDefaultAsync(m => m.ID == id);

            if (Paciente == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");
            UsuarioActual = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuarioActual);


            Atencion Atencion = new Atencion();
            Atencion.Paciente = Paciente;
            Atencion.Medico = UsuarioActual;
            Atencion.Titulo = TituloAtencion;
            Atencion.FechaHora = FechaAtencion;
            Atencion.Descripcion = DescripcionAtencion;

            _context.Atencion.Add(Atencion);

            await _context.SaveChangesAsync();

            return RedirectToPage("./ComenzarAtencion");
        }
    }
}