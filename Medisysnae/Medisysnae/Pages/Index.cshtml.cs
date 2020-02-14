using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Medisysnae.Data;
using Medisysnae.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

//Es el login de la aplicacion..

namespace Medisysnae.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public IndexModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            HttpContext.Session.Clear();
            return Page();
        }


            [BindProperty]
        public Profesional Profesional { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Profesional.NombreUsuario = Request.Form["email"];
            Profesional.Password = Request.Form["pass"];
           

            Profesional prof = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == Profesional.NombreUsuario);

            if (prof == null)
            {
                TempData["ErrorLogin"] = "Usuario o contraseña incorrectos";
                return Page();
            }
            else if (Profesional.Password != prof.Password)
            {
                TempData["ErrorLogin"] = "Usuario o contraseña incorrectos";
                return Page();
            }
            else if (prof.EstaActivo == false)
            {
                TempData["ErrorLogin"] = "El usuario esta inhabilitado";
                return Page();
            }

            HttpContext.Session.SetString("NombreUsuarioActual", prof.NombreUsuario);

            return RedirectToPage("/Menu");
        }
    }
}

