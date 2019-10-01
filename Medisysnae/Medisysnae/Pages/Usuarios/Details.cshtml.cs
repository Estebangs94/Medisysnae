using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Medisysnae.Data;
using Medisysnae.Models;

namespace Medisysnae.Pages.Usuarios
{
    public class DetailsModel : PageModel
    {
        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public DetailsModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public Usuario Usuario { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.ID == id);

            if (Usuario == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
