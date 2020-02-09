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

namespace Medisysnae.Pages.Pacientes
{
    public class CreateModel : PageModel
    {
        public SelectList ObraSocialList { get; set; }
        public IList<Obrasocial> ObrasSociales { get; set; }
        [BindProperty]
        public IList<Antecedente> Antecedentes { get; set; }
        public Profesional UsuarioActual { get; set; }

        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public CreateModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        private void CargarOS()
        {
            ObrasSociales = _context.Obrasocial.OrderBy(i => i.Nombre)
                .ToList();
            ObraSocialList = new SelectList(ObrasSociales, "ID", "Nombre", null);
        }

        public IActionResult OnGet()
        {
            CargarOS();
            return Page();
        }

        [BindProperty]
        public Paciente Paciente { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");
            UsuarioActual = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuarioActual);

            Paciente.FechaCreacion = DateTime.Now.Date;

            Paciente.Obrasocial_ID = Paciente.Obrasocial.ID;
            Paciente.Obrasocial = null;

            Antecedentes = await _context.Antecedente
                .Where(a => a.EstaActivo == true)
                .ToListAsync();

            Paciente.Medico = UsuarioActual;

            _context.Paciente.Add(Paciente);

          

            foreach (Antecedente a in Antecedentes)
            {
                Antecedentespaciente ap = new Antecedentespaciente();
                ap.Antecedente = a;
                ap.Paciente = Paciente;
                ap.Medico = UsuarioActual;
                ap.ValorString = "";

                _context.AntecedentePaciente.Add(ap);
            }

                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            
        
        }
    }
}