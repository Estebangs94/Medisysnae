using System;
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
    public class IndexModel : PageModel
    {
        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public IndexModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public IList<Profesional> Profesional { get;set; }
        public Profesional UsuarioActual { get; set; }

        public async Task OnGetAsync()
        {
            //Con esto siempre recuperamos el UsuarioActual en cualquier pagina...
            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");
            UsuarioActual = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuarioActual);

            Profesional = await _context.Profesional
                .Where(p => p.EstaActivo == true)
                .ToListAsync();
        }
    }
}
