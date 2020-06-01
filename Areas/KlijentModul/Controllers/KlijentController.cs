
using eKnjige.Data;
using eKnjige.Helpers;
using eKnjige.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace eKnjige.Controllers
{
    [Area("KlijentModul")]
    public class KlijentController : Controller
    {
        private readonly AppContext _db;
        public KlijentController(AppContext db)
        {

            _db = db;
        }
        public IActionResult Registracija()
        {
            List<Grad> gradovi = _db.Gradovi.ToList();
            List<SelectListItem> padajucaListagrad = new List<SelectListItem>();


            padajucaListagrad.Add(new SelectListItem() { Value = string.Empty, Text = "Odaberite Grad" });


            padajucaListagrad.AddRange(gradovi.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Naziv }));

            ViewData["gradovi"] = padajucaListagrad;

            List<Spol> spol = _db.Spol.ToList();
            List<SelectListItem> padajucaListaspol = new List<SelectListItem>();


            padajucaListaspol.AddRange(spol.Select(x => new SelectListItem() { Value = x.SpolID.ToString(), Text = x.Tip }));

            ViewData["spol"] = padajucaListaspol;

            return View(new Klijent());
        }

        [HttpPost]
        public IActionResult Registracija(Klijent klijent)
        {

            if (!ModelState.IsValid)
            {
                List<Grad> gradovi = _db.Gradovi.ToList();
                List<SelectListItem> padajucaListagrad = new List<SelectListItem>();


                padajucaListagrad.Add(new SelectListItem() { Value = string.Empty, Text = "Odaberite Grad" });


                padajucaListagrad.AddRange(gradovi.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Naziv }));

                ViewData["gradovi"] = padajucaListagrad;

                List<Spol> spol = _db.Spol.ToList();
                List<SelectListItem> padajucaListaspol = new List<SelectListItem>();


                padajucaListaspol.AddRange(spol.Select(x => new SelectListItem() { Value = x.SpolID.ToString(), Text = x.Tip }));

                ViewData["spol"] = padajucaListaspol;

                return View(klijent);

            }
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] b = System.Text.Encoding.UTF8.GetBytes(klijent.Lozinka);
            b = md5.ComputeHash(b);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach(byte x in b )
            {
                sb.Append(x.ToString("x2"));
              
            }
            klijent.Lozinka = sb.ToString();

            _db.Klijenti.Add(klijent);
            _db.SaveChanges();
            HttpContext.Setlogiranikorisnik(klijent);
            return RedirectToAction("Index", "KlijentProfil", new { area = "KlijentModul" });

        }

        public IActionResult DodajDrzavu()
        {

            return View(new Drzava());



        }


        [HttpPost]
        public IActionResult DodajDrzavu(Drzava drzava)
        {

            _db.Drzave.Add(drzava);
            _db.SaveChanges();
            return RedirectToAction("DodajGrad");

        }

        public IActionResult DodajGrad()
        {
            List<Drzava> drzave = _db.Drzave.ToList();


            List<SelectListItem> padajucaLista = new List<SelectListItem>();


            padajucaLista.Add(new SelectListItem() { Value = string.Empty, Text = "Odaberite Drzavu" });


            padajucaLista.AddRange(drzave.Select(x => new SelectListItem() { Value = x.DrzavaID.ToString(), Text = x.Naziv }));

            ViewData["Drzave"] = padajucaLista;
            return View(new Grad());



        }

        [HttpPost]
        public IActionResult DodajGrad(Grad grad)
        {

            _db.Gradovi.Add(grad);


            _db.SaveChanges();


            return RedirectToAction("Registracija");
        }
    }
}