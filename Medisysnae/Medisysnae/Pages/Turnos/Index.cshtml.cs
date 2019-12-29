using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Medisysnae.Models;
using Microsoft.EntityFrameworkCore;

namespace Medisysnae.Pages.Turnos
{
    public class IndexModel : PageModel
    {

        private readonly Medisysnae.Data.MedisysnaeContext _context;

        [BindProperty]
        public Profesional UsuarioActual { get; set; }

        public Agenda Agenda { get; set; }

        public async Task<IActionResult> OnGet()
        {
            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");

            if (NombreUsuarioActual == null)
            {
                //caduco la sesion
                Page().StatusCode = 401;
                return Page();
            }

            UsuarioActual = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuarioActual);

            return Page();
        }

        public IndexModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }
    }
}