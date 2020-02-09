using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medisysnae.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Medisysnae.Pages.Reportes
{
    public class AtencionesModel : PageModel
    {
        [BindProperty]
        public Profesional UsuarioActual { get; set; }
        [BindProperty]
        public List<Atencion> Atenciones { get; private set; }
        public PaginatedList<Atencion> AtencionesPaginadas { get; set; }

        [BindProperty]
        public Reporte Reportes { get; set; }

        public IList<Obrasocial> ObraSocialesTodas { get; set; }
        public SelectList ObraSocialesList { get; set; }

        public SelectList PacientesList { get; set; }
        public List<Paciente> Pacientes { get; set; }

        public IList<Tratamiento> TratamientosTodos { get; set; }
        public SelectList TratamientosList { get; set; }

        private IQueryable<Atencion> _atencionesIQ { get; set; }

        private readonly Data.MedisysnaeContext _context;
        private const int pageSize = 7;

        public AtencionesModel(Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await BuscarUsuario();

            Atenciones = null;

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

        private async Task<IActionResult> GenerarReporte(int? pageIndex)
        {
            await BuscarUsuario();

            Atenciones = await _context.Atencion.Include(a => a.Paciente)
                         .Include(a => a.Medico)
                         .Where(a => a.Medico.NombreUsuario == UsuarioActual.NombreUsuario)
                         .ToListAsync();

            foreach (Atencion ate in Atenciones)
            {
                ate.Paciente.Obrasocial = await _context.Obrasocial.FirstOrDefaultAsync(m => m.ID == ate.Paciente.Obrasocial_ID);
            }

            Filtrar();

            _atencionesIQ = Atenciones.AsQueryable();

            AtencionesPaginadas = PaginatedList<Atencion>.CreateSync(
                _atencionesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            CargarCombos();
            return Page();
        }

        private void Filtrar()
        {
            if (Reportes.PacienteId != 0)
            {
                Atenciones = Atenciones.Where(a => a.Paciente.ID == Reportes.PacienteId).ToList();
            }

            if (Reportes.ObraSocialId != 0)
            {
                Atenciones = Atenciones.Where(a => a.Paciente.Obrasocial_ID == Reportes.ObraSocialId).ToList();
            }

            if (Reportes.TratamientoId != 0)
            {
                Atenciones = Atenciones.Where(a => a.Tratamiento_ID == Reportes.TratamientoId).ToList();
            }

            if (Reportes.FechaDesde > new DateTime(1901, 01, 01))
            {
                Atenciones = Atenciones.Where(a => a.FechaHora >= Reportes.FechaDesde).ToList();
            }

            if (Reportes.FechaHasta > new DateTime(1901, 01, 01))
            {
                Atenciones = Atenciones.Where(a => a.FechaHora <= Reportes.FechaHasta).ToList();
            }

            Atenciones = Atenciones.OrderBy(a => a.FechaHora).ToList();
        }

        private void CargarTratamientos()
        {
            TratamientosTodos = _context.Tratamiento.ToList();
            TratamientosList = new SelectList(TratamientosTodos, "ID", "Nombre", null);
        }

        private void CargarObraSociales()
        {
            ObraSocialesTodas = _context.Obrasocial.ToList();
            ObraSocialesList = new SelectList(ObraSocialesTodas, "ID", "Nombre", null);
        }


        private void CargarPacientes()
        {
            Pacientes = _context.Paciente.OrderBy(i => i.ApellidoNombre)
                .Include(m => m.Medico)
                .Where(p => p.Medico.NombreUsuario == UsuarioActual.NombreUsuario)
                .ToList();
            PacientesList = new SelectList(Pacientes, "ID", "ApellidoNombre", null);
        }

        private void CargarCombos()
        {
            CargarTratamientos();
            CargarObraSociales();
            CargarPacientes();
        }

        private async Task BuscarUsuario()
        {
            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");
            UsuarioActual = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuarioActual);
        }
    }
}