﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Medisysnae.Data;
using Medisysnae.Models;
using Microsoft.AspNetCore.Http;

namespace Medisysnae.Pages.Profesionales
{
    public class DetailsModel : PageModel
    {
        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public DetailsModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public Profesional Profesional { get; set; }
        public Profesional UsuarioActual { get; set;
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profesional = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == id);

            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");
            UsuarioActual = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuarioActual);

            if (Profesional == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
