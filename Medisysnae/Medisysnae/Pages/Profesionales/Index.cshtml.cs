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
    public class IndexModel : PageModel
    {
        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public IndexModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public IList<Profesional> Profesional { get;set; }

        public async Task OnGetAsync()
        {
            Profesional = await _context.Profesional.ToListAsync();
        }
    }
}
