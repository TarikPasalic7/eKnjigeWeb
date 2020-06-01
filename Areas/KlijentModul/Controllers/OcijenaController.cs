
using eKnjige.Data;
using eKnjige.Helpers;
using eKnjige.Models;
using eKnjige.VewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace eKnjige.Areas.KlijentModul.Controllers
{
    [Area("KlijentModul")]
    public class OcijenaController : Controller
    {
        private readonly AppContext _db;
        public OcijenaController(AppContext db)
        {

            _db = db;
        }

        public IActionResult Index(int knjigaId)
        {

            OcijeniVMIndex m = new OcijeniVMIndex();
            EKnjiga knjiga = _db.EKnjige.Find(knjigaId);
            m.KnjigaID = knjiga.EKnjigaID;
            m.naziv = knjiga.Naziv;
            m.slika = knjiga.Slika;
            m.ocijena = knjiga.OcjenaKnjige;
            return View(m);

        }

        public IActionResult SnimiOcijenuOdabir(int id)
        {

            SnimiOcijenuVM o = new SnimiOcijenuVM();

            o.KnjigaId = id;


            o.Ocijena = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

            for (int i = 5; i <= 10; i++)

            {
                o.Ocijena.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {



                    Value = i.ToString(),
                    Text = i.ToString()




                });

            }





            return PartialView(o);
        }

        public IActionResult SnimiOcijenu(SnimiOcijenuVM m)
        {

            var klijent = HttpContext.getKorisnickiNalog();

            int? ocijenakorisnik = _db.KlijentKnjigaOcijene.Where(x => x.EKnjigaID == m.KnjigaId && x.KlijentID == klijent.KlijentID).Select(x => x.KlijentKnjigaOcijenaID).FirstOrDefault();
            KlijentKnjigaOcijena k = _db.KlijentKnjigaOcijene.Find(ocijenakorisnik);
            if (k != null)
            {

                k.Ocjena = m.Ocijenavrijednost;
                k.DatumOcijene = System.DateTime.Now;

                _db.SaveChanges();


            }
            else
            {
                KlijentKnjigaOcijena k2 = new KlijentKnjigaOcijena();
                k2.KlijentID = klijent.KlijentID;
                k2.EKnjigaID = m.KnjigaId;
                k2.DatumOcijene = System.DateTime.Now;
                k2.Ocjena = m.Ocijenavrijednost;
                _db.KlijentKnjigaOcijene.Add(k2);
                _db.SaveChanges();

            }
            var knjiga = _db.EKnjige.Find(m.KnjigaId);
            var prosjek = _db.KlijentKnjigaOcijene.Where(x => x.EKnjigaID == m.KnjigaId).Average(x => (float?)x.Ocjena) ?? 10;
            knjiga.OcjenaKnjige = prosjek;
            _db.SaveChanges();
            SnimiOcijenuVM s = new SnimiOcijenuVM();
            s.prosijek = prosjek;
            s.KnjigaId = m.KnjigaId;


            return PartialView(s);
        }
    }
}