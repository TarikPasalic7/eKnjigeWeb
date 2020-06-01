
using eKnjige.Areas.AdministratorModul.ViewModels;
using eKnjige.Data;
using eKnjige.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace eKnjige.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class KorisnickiRacunController : Controller
    {
        private readonly AppContext _db;
        public KorisnickiRacunController(AppContext db)
        {
            _db = db;
        }
        public IActionResult Pregled()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DodajKorisnika()
        {

            KorisniciDodajVM model = new KorisniciDodajVM
            {
                
                Spol = _db.Spol.Select(k => new SelectListItem
                {
                    Value = k.SpolID.ToString(),
                    Text = k.Tip
                }).ToList(),
                Grad = _db.Gradovi.Select(k => new SelectListItem
                {
                    Value = k.Id.ToString(),
                    Text = k.Naziv
                }).ToList()
            };

            //model.Grad.Insert(0, new SelectListItem() { Value = string.Empty, Text = "Odaberite Grad" });
            //model.Spol.Insert(0, new SelectListItem() { Value = null, Text = "Odaberi spol" });
            return View(model);
        }
        [HttpPost]
        public IActionResult DodajKorisnika(KorisniciDodajVM mod)
        {

            if (!ModelState.IsValid)
            {
          
            mod.Spol = _db.Spol.Select(k => new SelectListItem
                {
                    Value = k.SpolID.ToString(),
                    Text = k.Tip
                }).ToList();
                mod.Grad = _db.Gradovi.Select(k => new SelectListItem
                {
                    Value = k.Id.ToString(),
                    Text = k.Naziv
                }).ToList();
                //mod.Grad.Insert(0, new SelectListItem() { Value = string.Empty, Text = "Odaberite Grad" });
                //mod.Spol.Insert(0, new SelectListItem() { Value = null, Text = "Odaberi spol" });

                return View(mod);
            }

            Klijent klijent = new Klijent
            {
                Ime = mod.Ime,
                Prezime = mod.Prezime,
                DatumRodjenja = mod.DatumRodjenja,
                Email = mod.Email,
                Jmbg = mod.Jmbg,
                Lozinka = mod.Lozinka,
                KorisnickoIme = mod.KorisnickoIme,
                GradID = mod.GradID,
                SpolID = mod.SpolID

            };
            _db.Add(klijent);
            _db.SaveChanges();
          
           
            return RedirectToAction("Korisnici");
        }
        [HttpGet]
        public IActionResult UrediKorisnika(int KorisnikID)
        {
          
            Klijent x = _db.Klijenti.SingleOrDefault(k => k.KlijentID == KorisnikID);
            KorisniciUrediVM model = new KorisniciUrediVM
            {
                KlijentID = x.KlijentID,
                Email = x.Email,
                Ime = x.Ime,
                Prezime = x.Prezime,
                Jmbg = x.Jmbg,
                Lozinka=x.Lozinka,
                DatumRodjenja=x.DatumRodjenja,
                Spol = _db.Spol.Select(k => new SelectListItem
                {
                    Value = k.SpolID.ToString(),
                    Text = k.Tip
                }).ToList(),
                KorisnickoIme = x.KorisnickoIme,
                Grad = _db.Gradovi.Select(k => new SelectListItem
                {
                    Value = k.Id.ToString(),
                    Text = k.Naziv
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult UrediKorisnika(KorisniciUrediVM mod)
        {
            var korisnik = _db.Klijenti.SingleOrDefault(x => x.KlijentID == mod.KlijentID);
            if (!ModelState.IsValid)
            {
                mod.Spol = _db.Spol.Select(k => new SelectListItem
                {
                    Value = k.SpolID.ToString(),
                    Text = k.Tip
                }).ToList();
                mod.Grad = _db.Gradovi.Select(k => new SelectListItem
                {
                    Value = k.Id.ToString(),
                    Text = k.Naziv
                }).ToList();
                return View(mod);
            }
            korisnik.Ime = mod.Ime;
            korisnik.Prezime = mod.Prezime;
            korisnik.DatumRodjenja = mod.DatumRodjenja;
            korisnik.Email = mod.Email;
            korisnik.Jmbg = mod.Jmbg;
            korisnik.Lozinka = mod.Lozinka;
            korisnik.KorisnickoIme = mod.KorisnickoIme;
            korisnik.GradID = mod.GradID;
            korisnik.SpolID = mod.SpolID;

            
            _db.Klijenti.Update(korisnik);
            _db.SaveChanges();


            return RedirectToAction("Korisnici");
        }
        public IActionResult Administratori()
        {
            List<Administrator> administrator = _db.Administratori
                .Select(a => new Administrator
                {
                    KorisnickoIme = a.KorisnickoIme,
                    Ime = a.Ime,
                    Prezime = a.Prezime,
                    Email = a.Email
                }).ToList();


            ViewData["administrator-kljuc"] = administrator;
            return View();
        }
        public IActionResult Korisnici(string pretragaString)
        {

            if (string.IsNullOrEmpty(pretragaString))
            {
                List<Klijent> korisnik = _db.Klijenti


                .Select(k => new Klijent
                {
                    KorisnickoIme = k.KorisnickoIme,
                    Ime = k.Ime,
                    Prezime = k.Prezime,
                    Email = k.Email,
                    KlijentID = k.KlijentID
                }).ToList();
                ViewData["korisnik-kljuc"] = korisnik;
            }
            else
            {
                List<Klijent> korisnik = _db.Klijenti


                .Select(k => new Klijent
                {
                    KorisnickoIme = k.KorisnickoIme,
                    Ime = k.Ime,
                    Prezime = k.Prezime,
                    Email = k.Email,
                    KlijentID = k.KlijentID
                }).Where(x => x.Ime.ToLower().Contains(pretragaString.ToLower())).ToList();
                ViewData["korisnik-kljuc"] = korisnik;
            }



            return View();
        }
        public IActionResult Obrisi(int id)
        {
            Klijent x = _db.Klijenti.Find(id);
            _db.Klijenti.Remove(x);
            _db.SaveChanges();
            return RedirectToAction("Korisnici");
        }
    }
}