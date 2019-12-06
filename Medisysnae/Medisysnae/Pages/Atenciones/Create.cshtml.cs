using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Medisysnae.Data;
using Medisysnae.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Medisysnae.Pages.Atenciones
{
    public class CreateModel : PageModel
    {
        public Profesional UsuarioActual { get; set; }

        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public CreateModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }


        public IActionResult OnGet()
        {
            return Page();
        }
    }
}