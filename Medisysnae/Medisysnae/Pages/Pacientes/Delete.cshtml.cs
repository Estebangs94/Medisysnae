using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Medisysnae.Data;
using Medisysnae.Models;

namespace Medisysnae.Pages.Pacientes
{
    public class DeleteModel : PageModel
    {
        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public DeleteModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Paciente Paciente { get; set; }

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

            await cargarOSPaciente();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Paciente = await _context.Paciente.FindAsync(id);

            if (Paciente != null)
            {
                Paciente.EstaActivo = false;
                _context.Attach(Paciente).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }

            return RedirectToPage("./Index");
        }

        private async Task cargarOSPaciente()
        {
            Paciente.Obrasocial = await _context.Obrasocial.FirstOrDefaultAsync(m => m.ID == Paciente.Obrasocial_ID);
        }
    }
}
