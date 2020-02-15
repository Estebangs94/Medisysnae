using Medisysnae.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
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
        private IHostingEnvironment _hostingEnvironment;

        private const int pageSize = 7;

        public PacientesModel(Data.MedisysnaeContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
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

        public async Task<IActionResult> OnPostExportar()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"pacientes.xlsx";
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

                row.CreateCell(0).SetCellValue("Fecha creacion");
                row.CreateCell(1).SetCellValue("Paciente");
                row.CreateCell(2).SetCellValue("Obra social");
                row.CreateCell(3).SetCellValue("Comentario");

                for (int i = 0; i < Pacientes.Count; i++)
                {
                    row = excelSheet.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(Pacientes[i].FechaCreacionString);
                    row.CreateCell(1).SetCellValue(Pacientes[i].ApellidoNombre);
                    row.CreateCell(2).SetCellValue(Pacientes[i].Obrasocial.Nombre);
                    row.CreateCell(3).SetCellValue(Pacientes[i].Comentario);

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

            Pacientes = await _context.Paciente.Include(a => a.Medico)
                         .Where(a => a.Medico.NombreUsuario == UsuarioActual.NombreUsuario && a.EstaActivo == true)
                         .ToListAsync();

            foreach (Paciente pac in Pacientes)
            {
                pac.Obrasocial = await _context.Obrasocial.FirstOrDefaultAsync(m => m.ID == pac.Obrasocial_ID);
                pac.FechaCreacionString = pac.FechaCreacion.ToString("dd/MM/yyyy");
            }

            Filtrar();
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
                         .Where(a => a.Medico.NombreUsuario == UsuarioActual.NombreUsuario && a.EstaActivo == true)
                         .ToListAsync();

            foreach (Paciente pac in Pacientes)
            {
                pac.Obrasocial = await _context.Obrasocial.FirstOrDefaultAsync(m => m.ID == pac.Obrasocial_ID);
                pac.FechaCreacionString = pac.FechaCreacion.ToString("dd/MM/yyyy");
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