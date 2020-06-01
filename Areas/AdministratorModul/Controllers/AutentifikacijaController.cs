using eKnjige.Areas.AdministratorModul.ViewModels;
using eKnjige.Data;
using eKnjige.Helpers;
using eKnjige.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit;
namespace eKnjige.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class AutentifikacijaController : Controller
    {
        private readonly AppContext _db;
 
        public AutentifikacijaController(AppContext db)
        {
            _db = db;
         
        }
        public IActionResult Index()
        {
            return View(new LoginVM() { Zapamptipassword = true, }); ;
        }

        [HttpPost]
        public IActionResult Login(LoginVM login)
        {
           
            if (!ModelState.IsValid)
            {
                return View("Index", login);

            }

            Administrator administrator = _db.Administratori.SingleOrDefault(k => k.KorisnickoIme == login.Username && k.Lozinka == login.Password);

            if (administrator == null)
            {
                ModelState.AddModelError("", "Korisnicko ime ili lozinka nisu tacni");
            }
            if (!ModelState.IsValid)
            {

                return View("Index", login);
            }
            
            var kod = RandomStringGenerator.RandomString(6);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("EKnjige", "eknjigeeknjige@gmail.com"));

            message.To.Add(new MailboxAddress(administrator.Ime, administrator.Email));

            message.Subject = "Potvrdni kod";

            message.Body = new TextPart("plain")
            {
                Text = "Poštovani,\n\n" +
                "Unesite sljedeci kod da bi ste pristupili sistemu: " + kod
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("eknjigeeknjige@gmail.com", "Administrator!");
                client.Send(message);
                client.Disconnect(true);
            }

            //HttpContext.SetlogiraniAdministrator(administrator);
            AutentifikacijaKodVM kodvm = new AutentifikacijaKodVM() {
                adminId = administrator.AdministratorID,
                kodGen = kod,
                
            };

      
            return View("UnosKoda", kodvm);

        
            //return RedirectToAction("Index", "Administrator", new { area = "AdministratorModul" });
            //Autentifikacija.PokreniNovuSesiju(nalog, httpContext.HttpContext);
            //return RedirectToAction("Index", "Klijent/Rezervacije");

        }
        public IActionResult ProvjeraKoda(AutentifikacijaKodVM input)
        {
          if(input.kodGen==input.kodKor)
            {
                Administrator administrator = _db.Administratori.SingleOrDefault(a=>a.AdministratorID==input.adminId);
                HttpContext.SetlogiraniAdministrator(administrator);
                return RedirectToAction("Index", "Administrator", new { area = "AdministratorModul" });
            }
            else
            {
                input.kodKor = null;
                ModelState.AddModelError("","Uneseni kod nije ispravan");
                
                return View("UnosKoda",input);
            }
            

        }

        

   
        public IActionResult Logout()
        {

            HttpContext.Session.Clear(); //Session.Remove 
            return RedirectToAction("Index");
        }

    }
}