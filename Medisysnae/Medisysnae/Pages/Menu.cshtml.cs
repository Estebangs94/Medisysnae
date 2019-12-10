using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Medisysnae.Models;

namespace Medisysnae.Pages
{
    public class MenuModel : PageModel
    {
        private readonly Medisysnae.Data.MedisysnaeContext _context;

        [BindProperty]
        public Profesional UsuarioActual { get; set; }

        public async Task OnGetAsync()
        {
            //Configurar recuperacion de usuario para mostrar Datos Login

            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");
            UsuarioActual = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuarioActual);
        }

        public MenuModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }
        
    }
}