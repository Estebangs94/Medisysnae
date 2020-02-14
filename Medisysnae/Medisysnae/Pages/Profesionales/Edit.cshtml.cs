using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Medisysnae.Data;
using Medisysnae.Models;
using Microsoft.AspNetCore.Http;

namespace Medisysnae.Pages.Profesionales
{
    public class EditModel : PageModel
    {
        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public EditModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Profesional Profesional { get; set; }
        public Profesional UsuarioActual { get; set; }
        [BindProperty]
        public int LugarAtencionID { get; set; }

        [BindProperty]
        public string AregarLugarAtencion { get; set; }

        public List<LugaresAtencion> LugaresAtencionProf { get; set; }
        public SelectList LugaresAtencionList { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profesional = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == id);
            Profesional.MostrarPass = Profesional.Password;

            await cargarLugaresAtencion();

            await CargarUsuarioActual();

            if (Profesional == null)
            {
                return NotFound();
            }
            return Page();
        }

        private async Task cargarLugaresAtencion()
        {
            LugaresAtencionProf = await _context.LugaresAtencion.Where(l => l.UsuarioProfesional == Profesional.NombreUsuario && l.EstaActivo)
                                                .ToListAsync();
            LugaresAtencionList = new SelectList(LugaresAtencionProf, "ID", "Lugar", null);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Profesional.Password = Profesional.MostrarPass;
            ModelState.Remove("Profesional.Password");
            if (!ModelState.IsValid)
            {
                await CargarUsuarioActual();

                return Page();
            }

            _context.Attach(Profesional).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesionalExists(Profesional.NombreUsuario))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task CargarUsuarioActual()
        {
            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");
            UsuarioActual = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuarioActual);
        }

        public async Task<IActionResult> OnPostBorrarLugarAtencion()
        {
            LugaresAtencion lugar = await _context.LugaresAtencion.FindAsync(LugarAtencionID);
            lugar.EstaActivo = false;
            _context.Attach(lugar).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await cargarLugaresAtencion();
            await CargarUsuarioActual();
            ModelState.Remove("Profesional.Password");

            return Page();
        }

        public async Task<IActionResult> OnPostAgregarLugarAtencion()
        {
            IList<LugaresAtencion> lugares = await _context.LugaresAtencion
                                            .Where(l => l.EstaActivo && l.UsuarioProfesional == Profesional.NombreUsuario)
                                            .ToListAsync();

            foreach (var item in lugares)
            {
                if(item.Lugar == AregarLugarAtencion)
                {
                    ModelState.Remove("Profesional.Password");
                    ModelState.AddModelError("LugarDuplicado", $"Usted ya tiene como lugar de atención: {item.Lugar}");
                    await cargarLugaresAtencion();
                    await CargarUsuarioActual();
                    return Page();
                }
            }

            LugaresAtencion LugarToAdd = new LugaresAtencion();
            LugarToAdd.Lugar = AregarLugarAtencion;
            LugarToAdd.EstaActivo = true;
            LugarToAdd.UsuarioProfesional = Profesional.NombreUsuario;

            _context.LugaresAtencion.Add(LugarToAdd);
            await _context.SaveChangesAsync();
            await cargarLugaresAtencion();
            await CargarUsuarioActual();
            ModelState.Remove("Profesional.Password");

            return Page();
        }

        private bool ProfesionalExists(string id)
        {
            return _context.Profesional.Any(e => e.NombreUsuario == id);
        }
    }
}
