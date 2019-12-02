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
        public IList<Antecedente> Antecedentes { get; set; }

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

            IQueryable<Antecedente> antecedentesIQ = from a in _context.Antecedente
                                                     select a;

            antecedentesIQ = antecedentesIQ.Where(a => a.EstaActivo == true);
            antecedentesIQ = antecedentesIQ.OrderBy(a => a.Orden);

            Antecedentes = await antecedentesIQ.AsNoTracking().ToListAsync();

            return Page();
        }
    }
}