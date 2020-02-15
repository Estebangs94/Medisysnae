using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Medisysnae.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

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
        public List<LugaresAtencion> LugaresAtencion { get; set; }

        public SelectList EstadosList { get; set; }
        public List<string> Estados { get; set; }

        private IQueryable<Turno> _turnosIQ { get; set; }

        private readonly Data.MedisysnaeContext _context;
        private IHostingEnvironment _hostingEnvironment;

        public TurnosModel(Data.MedisysnaeContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
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

            Turnos = Turnos
                    .OrderBy(t => t.HoraComienzo)
                    .OrderBy(a => a.FechaTurno)
                    .ToList();
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
            LugaresAtencion = _context.LugaresAtencion
                              .Where(l => l.EstaActivo && l.UsuarioProfesional == UsuarioActual.NombreUsuario)
                              .ToList();
                        
            LugaresAtencionList = new SelectList(LugaresAtencion, "Lugar", "Lugar", null);
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

        public async Task<IActionResult> OnPostExportar()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"turnos.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Reporte");
                IRow row = excelSheet.CreateRow(0);

                await GenerarDatosExportacion();

                foreach (var item in Turnos)
                {
                    item.FechaTurnoString = item.FechaTurno.ToString("dd/MM/yyyy");
                }

                row.CreateCell(0).SetCellValue("Fecha turno");
                row.CreateCell(1).SetCellValue("Hora inicio");
                row.CreateCell(2).SetCellValue("Hora fin");
                row.CreateCell(3).SetCellValue("Paciente");
                row.CreateCell(4).SetCellValue("Obra social");
                row.CreateCell(5).SetCellValue("Estado");
                row.CreateCell(6).SetCellValue("Lugar de atención");

                for (int i = 0; i < Turnos.Count; i++)
                {
                    row = excelSheet.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(Turnos[i].FechaTurnoString);
                    row.CreateCell(1).SetCellValue(Turnos[i].HoraComienzo.ToString());
                    row.CreateCell(2).SetCellValue(Turnos[i].HoraFin.ToString());
                    row.CreateCell(3).SetCellValue(Turnos[i].Paciente.ApellidoNombre);
                    row.CreateCell(4).SetCellValue(Turnos[i].Paciente.Obrasocial.Nombre);
                    row.CreateCell(5).SetCellValue(Turnos[i].EstadoString);
                    row.CreateCell(6).SetCellValue(Turnos[i].LugarAtencion);

                }

                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }

        private async Task GenerarDatosExportacion()
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
        }
    }
}