using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Medisysnae.Data;
using Medisysnae.Models;

namespace Medisysnae.Pages.Profesionales
{
    public class CreateModel : PageModel
    {
        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public CreateModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Profesional Profesional { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Profesional.EstaActivo = true;
            Profesional.EsAdministrador = false;

            var Usuario = await _context.Profesional.FindAsync(Profesional.NombreUsuario);

            if(Usuario != null)
            {
                ModelState.AddModelError("UsuarioDuplicado", "El usuario que ha ingresado ya existe. Si olvido su contraseña recupérela");
                return Page();
            }

            _context.Profesional.Add(Profesional);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}