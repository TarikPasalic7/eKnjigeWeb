
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
    public class KategorijaController : Controller
    {
        private readonly AppContext _db;
        public KategorijaController(AppContext db)
        {

            _db = db;
        }
        public IActionResult Dodaj(string messagetext)
        {
     
           
                Kategorija kat = new Kategorija();
             
                kat.Naziv = messagetext;

                _db.Kategorije.Add(kat);
                _db.SaveChanges();

            


            return RedirectToAction("DodajKnjigu", "Knjiga", new { area = "AdministratorModul" });

        }
    }
}