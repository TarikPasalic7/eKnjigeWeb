
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eKnjige.Data;
using eKnjige.Models;
using eKnjige.Areas.KlijentModul.ViewModels;
using eKnjige.Helpers;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Microsoft.EntityFrameworkCore;

namespace eKnjige.Areas.KlijentModul.Controllers
{
    [Area("KlijentModul")]
    public class KupovinaController : Controller
    {
        private readonly AppContext _db;
        public int PageSize = 5;
        public KupovinaController(AppContext db)
        {

            _db = db;
        }

        public IActionResult Index()
        {

            KnjigeKupovinaVM m = new KnjigeKupovinaVM();
            m.rows = _db.EKnjige.Select(x => new KnjigeKupovinaVM.Row
            {

                KnjigaID = x.EKnjigaID,
                Naziv = x.Naziv,
                Cijena = x.Cijena,
                Ocjena = x.OcjenaKnjige,
                Slika = x.Slika



            }).ToList();

            m.kategorije = _db.Kategorije.ToList();
            m.tip = _db.TipFajlova.ToList();

           
          


            return View(m);
        }


        public IActionResult Knjigakupovina(int id)
        {

            ProizvodVM m = new ProizvodVM();


            EKnjiga k = _db.EKnjige.Find(id);
            m.KnjigaId = k.EKnjigaID;
            m.Naslov = k.Naziv;
            m.Ocjena = k.OcjenaKnjige;
            m.Cijena = k.Cijena;
            m.Slika = k.Slika;
            m.Opis = k.Opis;
            m.Komentari = _db.Komentari.Include(x=>x.Klijent).Where(x => x.EKnjigaID == k.EKnjigaID).ToList();
            m.rows = _db.EKnjigaAutori.Where(x => x.EKnjigaID == id).Select(y => new ProizvodVM.Row
            {
                Autor = y.Autor.Ime + " " + y.Autor.Prezime 
                

            }).ToList();




            m.rows2 = _db.EKnjigaKategorije.Where(x => x.EKnjigaID == id).Select(y => new ProizvodVM.Row2
            {

                Kategorija = y.Kategorija.Naziv

            }).ToList();



            m.rows3 = _db.EKnjigaTipovi.Where(x => x.EKnjigaID == id).Select(y => new ProizvodVM.Row3
            {
                TipFajla = y.Tipfajla.Naziv


            }).ToList();
            var korisnik = HttpContext.getKorisnickiNalog();

           
               
          



            return View(m);
        }

        public IActionResult Kupi(ProizvodVM p)
        {
            var korisnik = HttpContext.getKorisnickiNalog();
            if(korisnik==null)
            {
                return Redirect("/KlijentModul/Klijent/Registracija");
            }
            if(_db.KupovinaKnjiga.Where(x=>x.KlijentID==korisnik.KlijentID && x.EKnjigaID==p.KnjigaId).Any())
            {
                ViewData["Poruka"] = "Već ste kupili odabranu kjigu";
                return RedirectToAction("Index");

            }

            var customers = new CustomerService();
            var charges = new ChargeService();
            var customer = customers.Create(new CustomerCreateOptions { 
              Email=p.Email,
              Description=p.CardName,
              Source=p.StripeToken
            
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
             Amount= System.Convert.ToInt64( p.Cijena) *100,
             Description="Placeni iznos",
             Currency="bam",
             Customer=customer.Id

            });

            if(charge.Status=="succeeded")
            {

               
                KupovinaKnjige k = new KupovinaKnjige();
                k.KlijentID = korisnik.KlijentID;
                k.EKnjigaID = p.KnjigaId;
                k.DatumKupovine = System.DateTime.Now;
                _db.KupovinaKnjiga.Add(k);
                _db.SaveChanges();




                string balancetransactionid = charge.BalanceTransactionId;
                return Redirect("/KlijentModul/KlijentProfil/Index");

            }
            //else
            //{


            //}

            return View();
        }

        public IActionResult Komentar(ProizvodVM p)
        {

            var korisnik = HttpContext.getKorisnickiNalog();
            if (korisnik == null)
            {
                return Redirect("/KlijentModul/Klijent/Registracija");
            }

            Komentar kom = new Komentar();
            kom.EKnjigaID = p.KnjigaId;
            kom.KlijentID = korisnik.KlijentID;
            kom.DatumKomentara = System.DateTime.Now;
            kom.komentar = p.komentar;
            _db.Komentari.Add(kom);
            _db.SaveChanges();

            ProizvodVM m = new ProizvodVM();
     


            m.Komentari = _db.Komentari.Include(x => x.Klijent).Where(x => x.EKnjigaID == p.KnjigaId).ToList();






            return PartialView(m);
        }

        public IActionResult ObrisiKomentar(int id)
        {

            var korisnik = HttpContext.getKorisnickiNalog();
            if (korisnik == null)
            {
                return Redirect("/KlijentModul/Klijent/Registracija");
            }

            Komentar kom = _db.Komentari.Find(id);
            var knjigaid = kom.EKnjigaID;
            _db.Komentari.Remove(kom);
            _db.SaveChanges();
            ProizvodVM m = new ProizvodVM();



            m.Komentari = _db.Komentari.Include(x => x.Klijent).Where(x => x.EKnjigaID == knjigaid).ToList();






            return PartialView("Komentar",m);
        }

        public IActionResult KategorijaOdabir(int id)
        {

            KnjigeKupovinaVM m = new KnjigeKupovinaVM();
            m.rows = _db.EKnjigaKategorije.Where(x => x.KategorijaID == id).Select(x => new KnjigeKupovinaVM.Row
            {

                KnjigaID = x.EKnjigaID,
                Naziv = x.Eknjiga.Naziv,
                Cijena = x.Eknjiga.Cijena,
                Ocjena = x.Eknjiga.OcjenaKnjige,
                Slika = x.Eknjiga.Slika,
                
                


            }).ToList();



            return PartialView(m);
        }

        public IActionResult TipOdabir(int id)
        {
            KnjigeKupovinaVM m = new KnjigeKupovinaVM();
            m.rows = _db.EKnjigaTipovi.Where(x => x.TipFajlaID== id).Select(x => new KnjigeKupovinaVM.Row
            {

                KnjigaID = x.EKnjigaID,
                Naziv = x.Eknjiga.Naziv,
                Cijena = x.Eknjiga.Cijena,
                Ocjena = x.Eknjiga.OcjenaKnjige,
                Slika = x.Eknjiga.Slika

               


        }).ToList();

            return PartialView("KategorijaOdabir",m);
        }
        public IActionResult Trazi(KnjigeKupovinaVM k)
        {

            KnjigeKupovinaVM m = new KnjigeKupovinaVM();
            m.rows = _db.EKnjige.Where(x => x.Naziv.Contains(k.Trazi)).Select(x => new KnjigeKupovinaVM.Row
            {

                KnjigaID = x.EKnjigaID,
                Naziv = x.Naziv,
                Cijena = x.Cijena,
                Ocjena = x.OcjenaKnjige,
                Slika = x.Slika




            }).ToList();





            return PartialView("KategorijaOdabir", m);
        }
    }
}