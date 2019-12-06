using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Medisysnae.Data;
using Medisysnae.Models;

namespace Medisysnae.Pages.Atenciones
{
    public class IndexModel : PageModel
    {
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        private readonly Medisysnae.Data.MedisysnaeContext _context;

        public IndexModel(Medisysnae.Data.MedisysnaeContext context)
        {
            _context = context;
        }

        public PaginatedList<Paciente> Paciente { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Paciente> pacienteIQ = from p in _context.Paciente
                                              select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                pacienteIQ = pacienteIQ.Where(p => p.Apellido.Contains(searchString) || p.Nombre.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "name_desc":
                    pacienteIQ = pacienteIQ.OrderByDescending(p => p.Apellido);
                    break;
                case "Date":
                    pacienteIQ = pacienteIQ.OrderBy(p => p.Dni);
                    break;
                case "date_desc":
                    pacienteIQ = pacienteIQ.OrderByDescending(p => p.Dni);
                    break;
                default:
                    pacienteIQ = pacienteIQ.OrderBy(p => p.Apellido);
                    break;
            }

            int pageSize = 10;
            Paciente = await PaginatedList<Paciente>.CreateAsync(
                pacienteIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            //Paciente = await pacienteIQ.AsNoTracking().ToListAsync();
            foreach (Paciente pac in Paciente)
            {
                pac.Obrasocial = await _context.Obrasocial.FirstOrDefaultAsync(m => m.ID == pac.Obrasocial_ID);
            }
        }
    }
}