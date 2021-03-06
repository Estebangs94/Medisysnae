﻿using System;
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
    public class AntecedentesDetailsModel : PageModel
    {
        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public AntecedentesDetailsModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public Paciente Paciente { get; set; }
        public IList<Antecedentespaciente> AntecedentesPaciente { get; set; }

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

            AntecedentesPaciente = await _context.AntecedentePaciente
                .Include(i => i.Paciente)
                .Include(i => i.Antecedente)
                .Include(i => i.Medico)
                .OrderBy(i => i.Antecedente.Orden)
                .ToListAsync();

            AntecedentesPaciente = AntecedentesPaciente.Where(a => a.Paciente.ID == Paciente.ID)
                                    .ToList();

            return Page();
        }
    }
}