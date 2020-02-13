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
    public class DetailsModel : PageModel
    {
        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public DetailsModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

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

        private async Task cargarOSPaciente()
        {
            Paciente.Obrasocial = await _context.Obrasocial.FirstOrDefaultAsync(m => m.ID == Paciente.Obrasocial_ID);
        }
    }
}
