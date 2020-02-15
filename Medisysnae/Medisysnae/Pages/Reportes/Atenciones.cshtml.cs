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
        private IHostingEnvironment _hostingEnvironment;

        private const int pageSize = 7;

        public AtencionesModel(Data.MedisysnaeContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
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
                         .Where(a => a.Medico.NombreUsuario == UsuarioActual.NombreUsuario && a.EstaActiva == true)
                         .ToListAsync();



            foreach (Atencion ate in Atenciones)
            {
                ate.Paciente.Obrasocial = await _context.Obrasocial.FirstOrDefaultAsync(m => m.ID == ate.Paciente.Obrasocial_ID);
                ate.FechaHoraString = ate.FechaHora.ToString("dd/MM/yyyy");
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
                .Where(p => p.Medico.NombreUsuario == UsuarioActual.NombreUsuario  && p.EstaActivo == true)
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

        public async Task<IActionResult> OnPostExportar()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"atenciones.xlsx";
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

                row.CreateCell(0).SetCellValue("Fecha atención");
                row.CreateCell(1).SetCellValue("Paciente");
                row.CreateCell(2).SetCellValue("Obra social");
                row.CreateCell(3).SetCellValue("Tratamiento");
                row.CreateCell(4).SetCellValue("Motivo de consulta");
                row.CreateCell(5).SetCellValue("Comentario");

                for (int i = 0; i < Atenciones.Count; i++)
                {
                    row = excelSheet.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(Atenciones[i].FechaHoraString);
                    row.CreateCell(1).SetCellValue(Atenciones[i].Paciente.ApellidoNombre);
                    row.CreateCell(2).SetCellValue(Atenciones[i].Paciente.Obrasocial.Nombre);
                    row.CreateCell(3).SetCellValue(Atenciones[i].Tratamiento.Nombre);
                    row.CreateCell(4).SetCellValue(Atenciones[i].Titulo);
                    row.CreateCell(5).SetCellValue(Atenciones[i].Comentario);
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

            Atenciones = await _context.Atencion.Include(a => a.Paciente)
                         .Include(a => a.Medico)
                         .Where(a => a.Medico.NombreUsuario == UsuarioActual.NombreUsuario && a.EstaActiva == true)
                         .ToListAsync();



            foreach (Atencion ate in Atenciones)
            {
                ate.Paciente.Obrasocial = await _context.Obrasocial.FirstOrDefaultAsync(m => m.ID == ate.Paciente.Obrasocial_ID);
                ate.FechaHoraString = ate.FechaHora.ToString("dd/MM/yyyy");
                ate.Tratamiento = await _context.Tratamiento.FirstOrDefaultAsync(t => t.ID == ate.Tratamiento_ID);
            }

            Filtrar();
        }
    }
}