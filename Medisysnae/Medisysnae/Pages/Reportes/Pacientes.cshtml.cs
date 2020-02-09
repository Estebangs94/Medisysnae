using Medisysnae.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medisysnae.Pages.Reportes
{
    public class PacientesModel : PageModel
    {
        [BindProperty]
        public Profesional UsuarioActual { get; set; }

        [BindProperty]
        public Reporte Reportes { get; set; }

        [BindProperty]
        public List<Paciente> Pacientes { get; private set; }

        private IQueryable<Paciente> _pacientesIQ;

        public PaginatedList<Paciente> PacientesPaginados { get; set; }

        public IList<Obrasocial> ObraSocialesTodas { get; set; }
        public SelectList ObraSocialesList { get; set; }


        private readonly Data.MedisysnaeContext _context;
        private const int pageSize = 7;

        public PacientesModel(Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await BuscarUsuario();

            Pacientes = null;

            CargarCombos();

            return Page();
        }

        public async Task<IActionResult> OnPostGenerar(int? pageIndex)
        {
            return await GenerarReporte(pageIndex);
        }


        public async Task<IActionResult> OnPostGenerarNext(int? pageIndexNext)
        {
            return await GenerarReporte(pageIndexNext);
        }

        public async Task<IActionResult> OnPostGenerarPrev(int? pageIndexPrev)
        {
            return await GenerarReporte(pageIndexPrev);
        }

        private async Task BuscarUsuario()
        {
            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");
            UsuarioActual = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuarioActual);
        }

        private void CargarCombos()
        {
            CargarObraSociales();
        }

        private void CargarObraSociales()
        {
            ObraSocialesTodas = _context.Obrasocial.ToList();
            ObraSocialesList = new SelectList(ObraSocialesTodas, "ID", "Nombre", null);
        }

        private async Task<IActionResult> GenerarReporte(int? pageIndex)
        {
            await BuscarUsuario();

            Pacientes = await _context.Paciente.Include(a => a.Medico)
                         .Where(a => a.Medico.NombreUsuario == UsuarioActual.NombreUsuario)
                         .ToListAsync();

            foreach (Paciente pac in Pacientes)
            {
                pac.Obrasocial = await _context.Obrasocial.FirstOrDefaultAsync(m => m.ID == pac.Obrasocial_ID);
            }

            Filtrar();

            _pacientesIQ = Pacientes.AsQueryable();

            PacientesPaginados = PaginatedList<Paciente>.CreateSync(
                _pacientesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            CargarCombos();
            return Page();
        }

        private void Filtrar()
        {
            if (Reportes.ObraSocialId != 0)
            {
                Pacientes = Pacientes.Where(a => a.Obrasocial_ID == Reportes.ObraSocialId).ToList();
            }

            if (Reportes.FechaDesde > new DateTime(1901, 01, 01))
            {
                Pacientes = Pacientes.Where(a => a.FechaCreacion >= Reportes.FechaDesde).ToList();
            }

            if (Reportes.FechaHasta > new DateTime(1901, 01, 01))
            {
                Pacientes = Pacientes.Where(a => a.FechaCreacion <= Reportes.FechaHasta).ToList();
            }

            Pacientes = Pacientes.OrderBy(a => a.FechaCreacion).ToList();
        }
    }
}