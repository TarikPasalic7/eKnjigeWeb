using eKnjige.Data;
using eKnjige.Helpers;
using eKnjige.Models;
using eKnjige.VewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eKnjige.Areas.KlijentModul.ViewModels;
using System.Linq;
using X.PagedList;

using PagedList.Mvc;

namespace eKnjige.Areas.KlijentModul.Controllers
{

    [Area("KlijentModul")]
    public class KlijentProfilController : Controller
    {
        const int pagesize = 3;
        private readonly AppContext _db;
        public KlijentProfilController(AppContext db)
        {
            _db = db;
        }
               public IActionResult OdgovorNaPrijedlogPrikaz()
        {
            PrijedlogKnjigaPrikazVM model = new PrijedlogKnjigaPrikazVM
            {
                rows = _db.PrijedlogKnjiga.OrderBy(s => s.Odgovoren).Where(p => p.KlijentID == HttpContext.getKorisnickiNalog().KlijentID).Select(k => new PrijedlogKnjigaPrikazVM.Row
                {
                    PrijedlogKnjigeID = k.PrijedlogKnjigeID,
                    Datum = k.Datum,
                    Naziv = k.Naziv,
                    Administrator=k.Administrator.KorisnickoIme,
                    Odgovorio = k.Odgovoren,
                    Prihvatio = k.Prihvacen


                }).ToList()
            };
            return View(model);
            }
        public IActionResult Index(int? page)
        {
            Klijent kl = HttpContext.getKorisnickiNalog();
            Klijent k = _db.Klijenti.Include(g => g.Grad).Include(p => p.Spol).Where(pt => pt.KlijentID == kl.KlijentID).FirstOrDefault();


            //KlijentProfilVM model = new KlijentProfilVM();
            //{

                //model.Ime = k.Ime;
                //model.Prezime = k.Prezime;
                //model.Grad = k.Grad.Naziv;
                //model.Spol = k.Spol.Tip;
               var model = _db.KupovinaKnjiga.Where(x => x.KlijentID == k.KlijentID).Select(x=> new KupovinaKnjige { 
               EKnjigaID=x.EKnjigaID,
               EKnjiga=x.EKnjiga,
               KlijentID=x.KlijentID,
               Klijent=x.Klijent,
               DatumKupovine=x.DatumKupovine,
               KupovinaKnjigeID=x.KupovinaKnjigeID
               
               
               
               
               }).ToPagedList(page ?? 1, pagesize);
                //model.rows = _db.KupovinaKnjiga.Where(x => x.KlijentID == k.KlijentID).Select(x => new KlijentProfilVM.Row()
                //{
                //    KnjigaId = x.EKnjiga.EKnjigaID,
                //    NazivKnjige = x.EKnjiga.Naziv,
                //    ocijena = x.EKnjiga.OcjenaKnjige,
                //    slika = x.EKnjiga.Slika,
                //    mp3 = x.EKnjiga.Mp3Path,
                //    pdf = x.EKnjiga.PdfPath,
                //    AutorKnjige = _db.EKnjigaAutori.Where(autori => autori.EKnjigaID == x.EKnjigaID).Select(td => td.Autor.Ime + td.Autor.Prezime).FirstOrDefault()
                //}).ToList();
            //}

            return View(model);
        }

        [HttpPost]
        public IActionResult PredloziKnjigu(string messagetext)
        {
            System.Random rnd = new System.Random();
            Klijent k = HttpContext.getKorisnickiNalog();




            int klijentId = k.KlijentID;
            int adminid;
            Administrator isnull;
            do
            {
                adminid = _db.Administratori.Select(admin => rnd.Next(admin.AdministratorID, _db.Administratori.Max(f => f.AdministratorID))).FirstOrDefault();
                isnull = _db.Administratori.Find(adminid);

            } while (isnull == null);


            //int pkId = _db.PrijedlogKnjiga.Where(x => x.KlijentID == klijentId && x.AdministratorID == adminid).Select(x => x.PrijedlogKnjigeID).FirstOrDefault();
            //PrijedlogKnjiga pk = _db.PrijedlogKnjiga.Find(pkId);
            //if (pk != null)
            //{
            //    pk.Datum = System.DateTime.Now;
            //    pk.KlijentID = k.KlijentID;
            //    pk.AdministratorID = adminid;
            //    pk.Naziv = messagetext;
            //    _db.SaveChanges();
            //}
            //else
            //{
                PrijedlogKnjiga pk = new PrijedlogKnjiga();
                pk.KlijentID = klijentId;
                pk.AdministratorID = adminid;
                pk.Datum = System.DateTime.Now;
                pk.Naziv = messagetext;

                _db.PrijedlogKnjiga.Add(pk);
                _db.SaveChanges();

            //}


            return RedirectToAction("Index", "KlijentProfil", new { area = "KlijentModul" });

        }

    }
}