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
using System.ComponentModel.DataAnnotations;


namespace Medisysnae.Pages.Atenciones
{
    public class ComenzarAtencionModel : PageModel
    {
        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public ComenzarAtencionModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public Profesional UsuarioActual { get; set; }
        public Paciente Paciente { get; set; }

        [BindProperty]
        public string MedicacionAtencion { get; set; }

        [BindProperty]
        public string ComentarioAtencion { get; set; }

        [BindProperty]
        public int PacienteID { get; set; }

        [BindProperty]
        public string DescripcionAtencion { get; set; }

        [BindProperty]
        public string TituloAtencion { get; set; }

        //Efectivamente se pueden agregar DataAnotations en el code behind. Aprovechar para hacer validaciones..
        [BindProperty]
        [Display(Name = "Diagnóstico")]
        public string DiagnosticoAtencion { get; set; }

        [BindProperty]
        public DateTime FechaAtencion { get; set; }

        public IList<Antecedentespaciente> AntecedentesPaciente { get; set; }
        public IList<Atencion> AtencionesPaciente { get; set; }
        public IList<Tratamiento> TratamientosTodos { get; set; }

        public Atencion ate { get; set; }
        public SelectList TratamientosList { get; set; }

        [BindProperty]
        public Tratamiento tratamiento { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Paciente = await _context.Paciente.FirstOrDefaultAsync(m => m.ID == id);

            if (Paciente == null)
            {
                return NotFound();
            }

            PacienteID = Paciente.ID;

            await CargarDatos();
            return Page();
        }

        private async Task CargarDatos()
        {
            cargarTratamientos();

            AntecedentesPaciente = await _context.AntecedentePaciente
                            .Include(i => i.Paciente)
                            .Include(i => i.Antecedente)
                            .Include(i => i.Medico)
                            .OrderBy(i => i.Antecedente.Orden)
                            .Where(i => i.Paciente.ID == Paciente.ID)
                            .ToListAsync();

            AtencionesPaciente = await _context.Atencion
                .Include(i => i.Paciente)
                .Include(i => i.Medico)
                .OrderByDescending(i => i.FechaHora)
                .Where(i => i.EstaActiva == true)
                .Where(i => i.Paciente.ID == Paciente.ID)
                .ToListAsync();

            foreach (var item in AtencionesPaciente)
            {
                item.FechaHoraString = item.FechaHora.ToString("dd/MM/yyyy");
            }


            FechaAtencion = DateTime.Now;
            FechaAtencion = new DateTime(FechaAtencion.Year, FechaAtencion.Month, FechaAtencion.Day, FechaAtencion.Hour, FechaAtencion.Minute, 0, FechaAtencion.Kind);
        }

        private void cargarTratamientos()
        {
            TratamientosTodos = _context.Tratamiento.ToList();
            TratamientosList = new SelectList(TratamientosTodos, "ID", "Nombre", null);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string NombreUsuarioActual = HttpContext.Session.GetString("NombreUsuarioActual");
            UsuarioActual = await _context.Profesional.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuarioActual);

            Paciente = await _context.Paciente.FirstOrDefaultAsync(m => m.ID == PacienteID);

            Atencion Atencion = new Atencion();
            Atencion.Paciente = Paciente;
            Atencion.Medico = UsuarioActual;
            Atencion.Titulo = TituloAtencion;
            Atencion.FechaHora = FechaAtencion;
            Atencion.Descripcion = DescripcionAtencion;
            Atencion.Tratamiento_ID = tratamiento.ID;
            Atencion.Tratamiento = null;
            Atencion.Diagnostico = DiagnosticoAtencion;
            Atencion.Medicacion = MedicacionAtencion;
            Atencion.Comentario = ComentarioAtencion;
            Atencion.EstaActiva = true;

            _context.Atencion.Add(Atencion);

            await _context.SaveChangesAsync();

            await CargarDatos();
            return Page();
        }
    }
}