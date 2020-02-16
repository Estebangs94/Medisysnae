using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Medisysnae.Data;

namespace Medisysnae.Pages.Profesionales
{
    public class RetrieveUserDataModel : PageModel
    {
        private MedisysnaeContext _context;

        [BindProperty]
        public string NombreUsuario { get; set; }

        public RetrieveUserDataModel(MedisysnaeContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostRecuperar()
        {
            var UsuarioRecuperar = _context.Profesional
                                   .Where(p => p.NombreUsuario == NombreUsuario)
                                   .SingleOrDefault();

            if(UsuarioRecuperar == null)
            {
                ModelState.AddModelError("Error", "El email que ha ingresado no se corresponde con ningún usuario");
                return Page();
            }

            var fromAddress = new MailAddress("medisysnae@gmail.com", "Medisysnae");
            var toAddress = new MailAddress($"{NombreUsuario}");
            const string fromPassword = "$Medisysnae94";
            const string subject = "Recuperar cuenta";
            string body = $"A continuación le enviamos los datos de su cuenta, su usuario es: {UsuarioRecuperar.NombreUsuario} y su contraseña: {UsuarioRecuperar.Password}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

            return RedirectToPage("/Index");
        }
    }
}