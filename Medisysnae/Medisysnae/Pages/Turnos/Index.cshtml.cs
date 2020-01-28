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

namespace Medisysnae.Pages.Turnos
{
    public class IndexModel : PageModel
    {

        private readonly Medisysnae.Data.MedisysnaeContext _context;

        [BindProperty]
        public Profesional UsuarioActual { get; set; }
        
        [BindProperty]
        public DateTime DiaActual { get; set; }


        [BindProperty]
        public List<Turno> Turnos { get; set; }


        public IndexModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            DiaActual = DateTime.Now.Date;
            return await CargarTurnos(DiaActual);
        }

        public async Task<IActionResult> OnPostChangeDate(DateTime date)
        {          
            DiaActual = date;
            return await CargarTurnos(DiaActual);
        }

        private async Task<IActionResult> CargarTurnos(DateTime date)
        {
            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");

            if (NombreUsuarioActual == null)
            {
                //caduco la sesion
                Page().StatusCode = 401;
                return Page();
            }

            UsuarioActual = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuarioActual);

            IQueryable<Turno> turnoIQ = from t in _context.Turno
                                        select t;
            turnoIQ = turnoIQ.Where(t => t.NombreUsuario == UsuarioActual.NombreUsuario);
            turnoIQ = turnoIQ.Where(t => t.FechaTurno.Date == date.Date);

            Turnos = await turnoIQ.ToListAsync();

            foreach (var item in Turnos)
            {
                item.Paciente = await _context.Paciente.FirstOrDefaultAsync(m => m.ID == item.Paciente_ID);
            }

            return Page();
        }

    }
}