using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Medisysnae.Data;
using Medisysnae.Models;

namespace Medisysnae.Pages.Pacientes
{
    public class EditModel : PageModel
    {
        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public EditModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public SelectList ObraSocialList { get; set; }
        public IList<Obrasocial> ObrasSociales { get; set; }

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
            CargarOS();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CargarOS();
                return Page();
            }

            Paciente.EstaActivo = true;
            _context.Attach(Paciente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(Paciente.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PacienteExists(int id)
        {
            return _context.Paciente.Any(e => e.ID == id);
        }

        private async Task cargarOSPaciente()
        {
            Paciente.Obrasocial = await _context.Obrasocial.FirstOrDefaultAsync(m => m.ID == Paciente.Obrasocial_ID);
        }

        private void CargarOS()
        {
            ObrasSociales = _context.Obrasocial.OrderBy(i => i.Nombre)
                .ToList();
            ObraSocialList = new SelectList(ObrasSociales, "ID", "Nombre", Paciente.Obrasocial_ID);
        }
    }
}
