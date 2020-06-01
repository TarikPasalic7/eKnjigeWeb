
using eKnjige.Data;
using eKnjige.Helpers;
using eKnjige.Models;
using eKnjige.VewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using MimeKit;
using MailKit.Net.Smtp;
using eKnjige.Areas.KlijentModul.ViewModels;

namespace eKnjige.Controllers
{
    [Area("KlijentModul")]
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

        public IActionResult Login(LoginVM podaci)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] b = System.Text.Encoding.UTF8.GetBytes(podaci.Password);
            b = md5.ComputeHash(b);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (byte x in b)
            {
                sb.Append(x.ToString("x2"));

            }
            podaci.Password = sb.ToString();

            Klijent klijent = _db.Klijenti.SingleOrDefault(k => k.KorisnickoIme == podaci.Username && k.Lozinka == podaci.Password);

            if (klijent == null)
            {
                TempData["Error_Poruka"] = "Pogrešan username ili password";
                return View("Index", podaci);
            }

            var kod = RandomStringGenerator.RandomString(6);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("EKnjige", "eknjigeeknjige@gmail.com"));

            message.To.Add(new MailboxAddress(klijent.KorisnickoIme, klijent.Email));

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

            
            AutentifikacijaKodVM kodvm = new AutentifikacijaKodVM()
            {
                klijentId= klijent.KlijentID,
                kodGen = kod,

            };


            return View("UnosKoda", kodvm);



            //HttpContext.Setlogiranikorisnik(klijent);
            //return RedirectToAction("Index", "KlijentProfil", new { area = "KlijentModul" });

        }

        public IActionResult ProvjeraKoda(AutentifikacijaKodVM input)
        {
            if (input.kodGen == input.kodKor)
            {
                Klijent k = _db.Klijenti.SingleOrDefault(a => a.KlijentID == input.klijentId);
                HttpContext.Setlogiranikorisnik(k);
                return RedirectToAction("Index", "KlijentProfil", new { area = "KlijentModul" }); 
            }
            else
            {
                input.kodKor = null;
                ModelState.AddModelError("", "Uneseni kod nije ispravan");

                return View("UnosKoda", input);
            }


        }


        public IActionResult Logout()
        {

            HttpContext.Session.Clear(); //Session.Remove 
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}