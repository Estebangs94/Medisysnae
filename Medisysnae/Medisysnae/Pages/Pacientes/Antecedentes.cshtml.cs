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
    public class AntecedentesModel : PageModel
    {
        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public AntecedentesModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public Paciente Paciente { get; set; }

        [BindProperty]
        public int PacienteID { get; set; }
        [BindProperty]
        public int[] IDsAntecedentes { get; set; }

        [BindProperty]
        public string[] ValoresAntecedentes { get; set; }

        
        public IList<Antecedentespaciente> ListAntecedentes { get; set; }

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

            ListAntecedentes = await _context.AntecedentePaciente
                .Include(i => i.Paciente)
                .Include(i => i.Antecedente)
                .Include(i => i.Medico)
                .OrderBy(i => i.Antecedente.Orden)
                .ToListAsync();

            ListAntecedentes = ListAntecedentes.Where(a => a.Paciente.ID == Paciente.ID)
                                    .ToList();

            ValoresAntecedentes = new string[75];

            for (int i = 0; i < ListAntecedentes.Count(); i++)
            {
                ValoresAntecedentes[i] = ListAntecedentes[i].ValorString;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Paciente = await _context.Paciente.FirstOrDefaultAsync(m => m.ID == PacienteID);

            IList<Antecedentespaciente> antecedentes = await _context.AntecedentePaciente
                .Include(i => i.Paciente)
                .Include(i => i.Antecedente)
                .Include(i => i.Medico)
                .Where(i => i.Paciente.ID == PacienteID)
                .OrderBy(i => i.Antecedente.Orden)
                .ToListAsync();

            for (int i = 0; i < antecedentes.Count(); i++)
            {
                antecedentes[i].ValorString = ValoresAntecedentes[i];
                _context.Attach(antecedentes[i]).State = EntityState.Modified;
            }

           
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}