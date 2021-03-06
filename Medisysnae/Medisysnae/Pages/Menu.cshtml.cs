﻿using System;
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

        public async Task<IActionResult> OnGetAsync()
        {
            //Configurar recuperacion de usuario para mostrar Datos Login

            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");

            if (NombreUsuarioActual == null)
            {
                //caduco la sesion
                Page().StatusCode = 401;
                return Page();
            }

            UsuarioActual = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuarioActual);
            return Page();
        }

        public MenuModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }
        
    }
}