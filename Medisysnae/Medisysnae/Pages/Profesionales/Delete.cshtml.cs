using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Medisysnae.Data;
using Medisysnae.Models;

namespace Medisysnae.Pages.Profesionales
{
    public class DeleteModel : PageModel
    {
        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public DeleteModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Profesional Profesional { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profesional = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == id);

            if (Profesional == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profesional = await _context.Profesional.FindAsync(id);

            if (Profesional != null)
            {
                Profesional.EstaActivo = false;
                _context.Attach(Profesional).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
