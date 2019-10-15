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
            
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public string ErrorUsuario
        {
            get; set;
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
                TempData["ErrorLogin"] = true;
                return Page();
            }
            else if (Profesional.Password != prof.Password)
            {
                TempData["ErrorLogin"] = true;
                return Page();
            }

            Profesional = prof;

            return RedirectToPage("./Index");
        }
    }
}

