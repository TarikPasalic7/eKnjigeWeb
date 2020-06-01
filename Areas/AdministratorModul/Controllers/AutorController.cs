
using eKnjige.Areas.AdministratorModul.ViewModels;
using eKnjige.Data;
using eKnjige.Helpers;
using eKnjige.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace eKnjige.Areas.AdministratorModul.Controllers
{

    [Area("AdministratorModul")]
    public class AutorController : Controller
    {
        private readonly AppContext _db;
        public AutorController(AppContext db)
        {

            _db = db;
        }
        public IActionResult Dodaj(string ime, string prezime,System.DateTime datum)
        {


            Autor aut = new Autor();

            aut.Ime = ime;
            aut.Prezime = prezime;
            aut.Godiste = datum;

            _db.Autori.Add(aut);
            _db.SaveChanges();




            return RedirectToAction("DodajKnjigu", "Knjiga", new { area = "AdministratorModul" });

        }
    }
}