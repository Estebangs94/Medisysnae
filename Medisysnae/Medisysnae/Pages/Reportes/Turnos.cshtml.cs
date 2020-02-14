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
    public class TurnosModel : PageModel
    {
        [BindProperty]
        public Profesional UsuarioActual { get; set; }

        [BindProperty]
        public List<Turno> Turnos { get; private set; }
        public PaginatedList<Turno> TurnosPaginados { get; set; }
        public IQueryable<Turno> TurnosTodos { get; set; }

        [BindProperty]
        public Reporte Reportes { get; set; }

        public IList<Obrasocial> ObraSocialesTodas { get; set; }
        public SelectList ObraSocialesList { get; set; }

        public SelectList PacientesList { get; set; }
        public List<Paciente> Pacientes { get; set; }

        public SelectList LugaresAtencionList { get; set; }
        public IQueryable<string> LugaresAtencion { get; set; }

        public SelectList EstadosList { get; set; }
        public List<string> Estados { get; set; }

        private IQueryable<Turno> _turnosIQ { get; set; }

        private readonly Data.MedisysnaeContext _context;

        public TurnosModel(Data.MedisysnaeContext context)
        {
            _context = context;
        }

        private const int pageSize = 7;

        public async Task<IActionResult> OnGet()
        {
            await BuscarUsuario();

            Turnos = null;

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

            Turnos = await _context.Turno.Include(a => a.Paciente).Where(a => a.Paciente_ID != null)
                         .Where(a => a.NombreUsuario == UsuarioActual.NombreUsuario && a.EstaActivo == true)
                         .ToListAsync();

            foreach (Turno t in Turnos)
            {
                t.Paciente.Obrasocial = await _context.Obrasocial.FirstOrDefaultAsync(m => m.ID == t.Paciente.Obrasocial_ID);
            }

            Filtrar();

            _turnosIQ = Turnos.AsQueryable();

            TurnosPaginados = PaginatedList<Turno>.CreateSync(
                _turnosIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            CargarCombos();
            CargarFechaturno();
            return Page();
        }

        private void CargarFechaturno()
        {
            foreach (var item in TurnosPaginados)
            {
                item.FechaTurnoString = item.FechaTurno.ToString("dd/MM/yyyy");
            }
        }

        private void Filtrar()
        {
            if (Reportes.LugarAtencion != null)
            {
                Turnos = Turnos.Where(a => a.LugarAtencion == Reportes.LugarAtencion).ToList();
            }

            if (Reportes.Estado != null)
            {
                Turnos = Turnos.Where(a => a.EstadoString == Reportes.Estado).ToList();
            }

            if (Reportes.PacienteId != 0)
            {
                Turnos = Turnos.Where(a => a.Paciente.ID == Reportes.PacienteId).ToList();
            }

            if (Reportes.ObraSocialId != 0)
            {
                Turnos = Turnos.Where(a => a.Paciente.Obrasocial_ID == Reportes.ObraSocialId).ToList();
            }           

            if (Reportes.FechaDesde > new DateTime(1901, 01, 01))
            {
                Turnos = Turnos.Where(a => a.FechaTurno >= Reportes.FechaDesde).ToList();
            }

            if (Reportes.FechaHasta > new DateTime(1901, 01, 01))
            {
                Turnos = Turnos.Where(a => a.FechaTurno <= Reportes.FechaHasta).ToList();
            }

            Turnos = Turnos.OrderBy(a => a.FechaTurno).ToList();
        }

        private void CargarCombos()
        {
            cargarLugaresAtencion();
            cargarOS();
            cargarPacientes();
            cargarEstados();
        }

        private void cargarEstados()
        {
            Estados = new List<string>();

            Estados.Add(Turno.Estado.Atendido.ToString());
            Estados.Add(Turno.Estado.Bloqueado.ToString());
            Estados.Add(Turno.Estado.Cancelado.ToString());
            Estados.Add(Turno.Estado.Otorgado.ToString());

            EstadosList = new SelectList(Estados);
        }

        private void cargarLugaresAtencion()
        {
            LugaresAtencion = from t in _context.Turno
                          select t.LugarAtencion;
            LugaresAtencion = LugaresAtencion.Distinct();
                        
            LugaresAtencionList = new SelectList(LugaresAtencion);
        }

        private void cargarPacientes()
        {
            Pacientes = _context.Paciente.OrderBy(i => i.ApellidoNombre)
                .Include(m => m.Medico)
                .Where(p => p.Medico.NombreUsuario == UsuarioActual.NombreUsuario && p.EstaActivo == true)
                .ToList();
            PacientesList = new SelectList(Pacientes, "ID", "ApellidoNombre", null);
        }

        private void cargarOS()
        {
            ObraSocialesTodas = _context.Obrasocial.ToList();
            ObraSocialesList = new SelectList(ObraSocialesTodas, "ID", "Nombre", null);
        }

        private async Task BuscarUsuario()
        {
            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");
            UsuarioActual = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuarioActual);
        }
    }
}