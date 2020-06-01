
using eKnjige.Areas.AdministratorModul.ViewModels;
using eKnjige.Data;
using eKnjige.Helpers;
using eKnjige.Helpers.FileManager;
using eKnjige.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace eKnjige.Areas.AdministratorModul.Controllers
{

    [Area("AdministratorModul")]
    public class KnjigaController : Controller
    {
        private readonly Data.AppContext _db;
        private readonly IFileManager _fileManager;
        
        public int PageSize = 5;
        public KnjigaController(Data.AppContext db, IFileManager fileManager)
        {

            _db = db;
            _fileManager = fileManager;
        }

        [HttpGet]
        public IActionResult DodajKnjigu()
        {
            KnjigeDodajVM model = new KnjigeDodajVM
            {
                Autori = _db.Autori.Select(k => new SelectListItem
                {
                    Value = k.AutorID.ToString(),
                    Text = k.Ime + " " + k.Prezime
                }).ToList(),

                Tipovi = _db.TipFajlova.Select(t => new SelectListItem
                {
                    Value = t.TipFajlaID.ToString(),
                    Text = t.Naziv
                }).ToList(),

                Kategorije = _db.Kategorije.Select(k => new SelectListItem
                {
                    Value = k.KategorijaID.ToString(),
                    Text = k.Naziv
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DodajKnjigu(KnjigeDodajVM model)
        {
            if (model.PdfByte == null && model.Mp3Byte == null)
            {
                ModelState.AddModelError("PdfByte", "PDF ili mp3 fajl su obavezni");
            }

            if (model.TipoviId != null)
            {
                var tipovi = _db.TipFajlova;
                var pdfTipId = tipovi.SingleOrDefault(x => x.Naziv == "PDF").TipFajlaID;
                var mp3TipId = tipovi.SingleOrDefault(x => x.Naziv == "MP3").TipFajlaID;

                if (model.PdfByte == null && model.TipoviId.Contains(pdfTipId))
                {
                    ModelState.AddModelError("PdfByte", "PDF fajl je obavezan");
                }

                if (model.Mp3Byte == null && model.TipoviId.Contains(mp3TipId))
                {
                    ModelState.AddModelError("Mp3Byte", "MP3 fajl je obavezan");
                }

                if (model.PdfByte != null && !model.TipoviId.Contains(pdfTipId))
                {
                    ModelState.AddModelError("PdfByte", "Niste odabrali tip fajla PDF za uneseni PDF dokument");
                }

                if (model.Mp3Byte != null && !model.TipoviId.Contains(mp3TipId))
                {
                    ModelState.AddModelError("Mp3Byte", "Niste odabrali tip fajla MP3 za uneseni MP3 fajl");
                }
            }

            if (!ModelState.IsValid)
            {
                model.Autori = _db.Autori.Select(k => new SelectListItem
                {
                    Value = k.AutorID.ToString(),
                    Text = k.Ime + " " + k.Prezime
                }).ToList();

                model.Tipovi = _db.TipFajlova.Select(t => new SelectListItem
                {
                    Value = t.TipFajlaID.ToString(),
                    Text = t.Naziv
                }).ToList();

                model.Kategorije = _db.Kategorije.Select(k => new SelectListItem
                {
                    Value = k.KategorijaID.ToString(),
                    Text = k.Naziv
                }).ToList();

                Response.StatusCode = 400;
                return PartialView(model);
            }

            EKnjiga knjiga = new EKnjiga
            {
                Naziv = model.Naziv,
                AdministratorID = HttpContext.getKorisnickiNalogAdministrator().AdministratorID,
                Cijena = model.Cijena,
                Opis=model.Opis
                
            };

            if (model.SlikaByte != null)
            {
                knjiga.Slika = await _fileManager.Save(model.SlikaByte, "/Slike", $"{knjiga.Naziv}-{Guid.NewGuid()}.png");
            }

            if (model.PdfByte != null)
            {
                knjiga.PdfPath = await _fileManager.Save(model.PdfByte, "/PdfFajlovi", $"{knjiga.Naziv}-{Guid.NewGuid()}.pdf");
            }

            if (model.Mp3Byte != null)
            {
                knjiga.Mp3Path = await _fileManager.Save(model.Mp3Byte, "/Mp3Fajlovi", $"{knjiga.Naziv}-{Guid.NewGuid()}.mp3");
            }

            _db.EKnjige.Add(knjiga);
            _db.SaveChanges();

            foreach (var autorID in model.AutoriID)
            {
                var autor = new EKnjigeAutor
                {
                    AutorID = autorID,
                    EKnjigaID = knjiga.EKnjigaID
                };
                _db.EKnjigaAutori.Add(autor);
            }

            foreach (var tipFajlaId in model.TipoviId)
            {
                var tipFajla = new EKnjigaTip
                {
                    TipFajlaID = tipFajlaId,
                    EKnjigaID = knjiga.EKnjigaID
                };
                _db.EKnjigaTipovi.Add(tipFajla);
            }

            foreach (var kategorijaID in model.KategorijeID)
            {
                var kategorija = new EKnjigaKategorija
                {
                    KategorijaID = kategorijaID,
                    EKnjigaID = knjiga.EKnjigaID
                };
                _db.EKnjigaKategorije.Add(kategorija);
            }

            _db.SaveChanges();
            return Ok();
            
        }

        [HttpGet]
        public IActionResult IzmjeniKnjigu(int id)
        {
            var knjiga = _db.EKnjige.SingleOrDefault(x => x.EKnjigaID == id);

            KnjigaEditVM model = new KnjigaEditVM
            {
                EKnjigaId = knjiga.EKnjigaID,
                Naziv = knjiga.Naziv,
                Cijena = knjiga.Cijena,
                Opis=knjiga.Opis,
                AutoriID = _db.EKnjigaAutori.Where(x => x.EKnjigaID == id).Select(x => x.AutorID).ToArray(),
                KategorijeID = _db.EKnjigaKategorije.Where(x => x.EKnjigaID == id).Select(x => x.KategorijaID).ToArray(),
                TipoviId = _db.EKnjigaTipovi.Where(x => x.EKnjigaID == id).Select(x => x.TipFajlaID).ToArray(),

                Autori = _db.Autori.Select(k => new SelectListItem
                {
                    Value = k.AutorID.ToString(),
                    Text = k.Ime + " " + k.Prezime
                }).ToList(),

                Tipovi = _db.TipFajlova.Select(t => new SelectListItem
                {
                    Value = t.TipFajlaID.ToString(),
                    Text = t.Naziv
                }).ToList(),

                Kategorije = _db.Kategorije.Select(k => new SelectListItem
                {
                    Value = k.KategorijaID.ToString(),
                    Text = k.Naziv
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> IzmjeniKnjigu(KnjigaEditVM model)
        {
            var knjiga = _db.EKnjige.SingleOrDefault(x => x.EKnjigaID == model.EKnjigaId);
            if (model.TipoviId != null)
            {
                var tipovi = _db.TipFajlova;
                var pdfTipId = tipovi.SingleOrDefault(x => x.Naziv == "PDF").TipFajlaID;
                var mp3TipId = tipovi.SingleOrDefault(x => x.Naziv == "MP3").TipFajlaID;

                if (model.PdfByte == null && model.TipoviId.Contains(pdfTipId) && string.IsNullOrEmpty(knjiga.PdfPath))
                {
                    ModelState.AddModelError("PdfByte", "PDF fajl je obavezan");
                }

                if (model.Mp3Byte == null && model.TipoviId.Contains(mp3TipId) && string.IsNullOrEmpty(knjiga.Mp3Path))
                {
                    ModelState.AddModelError("Mp3Byte", "MP3 fajl je obavezan");
                }

                if (model.PdfByte != null && !model.TipoviId.Contains(pdfTipId))
                {
                    ModelState.AddModelError("PdfByte", "Niste odabrali tip fajla PDF za uneseni PDF dokument");
                }

                if (model.Mp3Byte != null && !model.TipoviId.Contains(mp3TipId))
                {
                    ModelState.AddModelError("Mp3Byte", "Niste odabrali tip fajla MP3 za uneseni MP3 fajl");
                }
            }

            if (!ModelState.IsValid)
            {
                model.Autori = _db.Autori.Select(k => new SelectListItem
                {
                    Value = k.AutorID.ToString(),
                    Text = k.Ime + " " + k.Prezime
                }).ToList();

                model.Tipovi = _db.TipFajlova.Select(t => new SelectListItem
                {
                    Value = t.TipFajlaID.ToString(),
                    Text = t.Naziv
                }).ToList();

                model.Kategorije = _db.Kategorije.Select(k => new SelectListItem
                {
                    Value = k.KategorijaID.ToString(),
                    Text = k.Naziv
                }).ToList();
                Response.StatusCode = 400;
                return PartialView(model);
            }

            knjiga.Naziv = model.Naziv;
            knjiga.Cijena = model.Cijena;
            knjiga.Opis = model.Opis;

            if (model.SlikaByte != null)
            {
                knjiga.Slika = await _fileManager.Save(model.SlikaByte, "/Slike", $"{knjiga.Naziv}-{Guid.NewGuid()}.png");
            }

            if (model.PdfByte != null)
            {
                knjiga.PdfPath = await _fileManager.Save(model.PdfByte, "/PdfFajlovi", $"{knjiga.Naziv}-{Guid.NewGuid()}.pdf");
            }

            if (model.Mp3Byte != null)
            {
                knjiga.Mp3Path = await _fileManager.Save(model.Mp3Byte, "/Mp3Fajlovi", $"{knjiga.Naziv}-{Guid.NewGuid()}.mp3");
            }

            _db.EKnjige.Update(knjiga);
            _db.SaveChanges();

            var knjige = _db.EKnjigaAutori.Where(x => x.EKnjigaID == model.EKnjigaId);
            _db.EKnjigaAutori.RemoveRange(knjige);
            foreach (var autorID in model.AutoriID)
            {
                var autor = new EKnjigeAutor
                {
                    AutorID = autorID,
                    EKnjigaID = knjiga.EKnjigaID
                };
                _db.EKnjigaAutori.Add(autor);
            }

            var tipoviKnjiga = _db.EKnjigaTipovi.Where(x => x.EKnjigaID == model.EKnjigaId);
            _db.EKnjigaTipovi.RemoveRange(tipoviKnjiga);
            foreach (var tipFajlaId in model.TipoviId)
            {
                var tipFajla = new EKnjigaTip
                {
                    TipFajlaID = tipFajlaId,
                    EKnjigaID = knjiga.EKnjigaID
                };
                _db.EKnjigaTipovi.Add(tipFajla);
            }

            var kategorije = _db.EKnjigaKategorije.Where(x => x.EKnjigaID == model.EKnjigaId);
            _db.EKnjigaKategorije.RemoveRange(kategorije);
            foreach (var kategorijaID in model.KategorijeID)
            {
                var kategorija = new EKnjigaKategorija
                {
                    KategorijaID = kategorijaID,
                    EKnjigaID = knjiga.EKnjigaID
                };
                _db.EKnjigaKategorije.Add(kategorija);
            }

            _db.SaveChanges(); 
            return Ok();
        }

        public IActionResult PrikaziKnjige(int page=1,string pretragaString = null)
        {
            KnjigeKategorijeAutoriTipVM model = new KnjigeKategorijeAutoriTipVM();
            if (string.IsNullOrEmpty(pretragaString))
            {

                model.htmlrows = _db.EKnjige.Select(k => new KnjigeKategorijeAutoriTipVM.Row()
                {
                    KnjigaID = k.EKnjigaID,
                    Cijena = k.Cijena,
                    Naziv = k.Naziv,
                    Slika = k.Slika,
                    OcijenaKnjige = _db.KlijentKnjigaOcijene.Where(s => s.EKnjigaID == k.EKnjigaID).Average(s => s.Ocjena),
                    Autori = string.Join(",\n", _db.EKnjigaAutori.Where(a => a.EKnjigaID == k.EKnjigaID).Select(q => q.Autor.Ime + ' ' + q.Autor.Prezime)),
                    Kategorije = string.Join(", \n", _db.EKnjigaKategorije.Where(n => n.EKnjigaID == k.EKnjigaID).Select(q => q.Kategorija.Naziv)),
                    TipFajlova = string.Join(", ", _db.EKnjigaTipovi.Where(a => a.EKnjigaID == k.EKnjigaID).Select(q => q.Tipfajla.Naziv))
                }).OrderBy(x => x.KnjigaID).Skip((page - 1) * PageSize).Take(PageSize).ToList();

                model.PagingInfo = new eKnjige.Areas.AdministratorModul.ViewModels.PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _db.EKnjige.Count()
                };
            }
            else
            {

                model.htmlrows = _db.EKnjige.Select(k => new KnjigeKategorijeAutoriTipVM.Row()
                {
                    KnjigaID = k.EKnjigaID,
                    Cijena = k.Cijena,
                    Naziv = k.Naziv,
                    Slika = k.Slika,
                    OcijenaKnjige = _db.KlijentKnjigaOcijene.Where(s => s.EKnjigaID == k.EKnjigaID).Average(s => s.Ocjena),
                    Autori = string.Join(",\n", _db.EKnjigaAutori.Where(a => a.EKnjigaID == k.EKnjigaID).Select(q => q.Autor.Ime + ' ' + q.Autor.Prezime)),
                    Kategorije = string.Join(", \n", _db.EKnjigaKategorije.Where(n => n.EKnjigaID == k.EKnjigaID).Select(q => q.Kategorija.Naziv)),
                    TipFajlova = string.Join(", ", _db.EKnjigaTipovi.Where(a => a.EKnjigaID == k.EKnjigaID).Select(q => q.Tipfajla.Naziv))
                }).Where(x => x.Naziv.Contains(pretragaString)).OrderBy(x => x.KnjigaID).Skip((page - 1) * PageSize).Take(PageSize).ToList();

                model.PagingInfo = new eKnjige.Areas.AdministratorModul.ViewModels.PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _db.EKnjige.Where(x => x.Naziv.Contains(pretragaString)).Count()
                };
            }




            //model.htmlrows = _db.EKnjige.Select(k => new KnjigeKategorijeAutoriTipVM.Row()
            //{
            //    KnjigaID = k.EKnjigaID,
            //    Cijena = k.Cijena,
            //    Naziv = k.Naziv,
            //    Slika = k.Slika,
            //    OcijenaKnjige = _db.KlijentKnjigaOcijene.Where(s => s.EKnjigaID == k.EKnjigaID).Average(s => s.Ocjena),
            //    Autori = string.Join(",\n", _db.EKnjigaAutori.Where(a => a.EKnjigaID == k.EKnjigaID).Select(q => q.Autor.Ime + ' ' + q.Autor.Prezime)),
            //    Kategorije = string.Join(", \n", _db.EKnjigaKategorije.Where(n => n.EKnjigaID == k.EKnjigaID).Select(q => q.Kategorija.Naziv)),
            //    TipFajlova = string.Join(", ", _db.EKnjigaTipovi.Where(a => a.EKnjigaID == k.EKnjigaID).Select(q => q.Tipfajla.Naziv))
            //}).Where(x => string.IsNullOrEmpty(pretragaString) || x.Naziv.ToLower().Contains(pretragaString.ToLower())).ToList().ToList();

            return View("PrikaziKnjige", model);
        }
        public IActionResult Obrisi(int id)
        {
            EKnjiga x = _db.EKnjige.Find(id);
            _db.EKnjige.Remove(x);
            _db.SaveChanges();
            return RedirectToAction("PrikaziKnjige");
        }
       
        public IActionResult ObrisiKomentar(int KomentarID)
        {
            Komentar x = _db.Komentari.Find(KomentarID);
            _db.Komentari.Remove(x);
            _db.SaveChanges();
    
            return RedirectToAction("PrikaziKnjige");
            
        }
        public IActionResult PrijedlogKnjigePrikaz()
        {

            PrijedlogKnjigaPrikazVM model = new PrijedlogKnjigaPrikazVM
            {
                rows = _db.PrijedlogKnjiga.OrderBy(s => s.Odgovoren).Where(p => p.AdministratorID == HttpContext.getKorisnickiNalogAdministrator().AdministratorID).Select(k => new PrijedlogKnjigaPrikazVM.Row
                {
                    PrijedlogKnjigeID = k.PrijedlogKnjigeID,
                    Datum = k.Datum,
                    Naziv = k.Naziv,
                    Klijent = k.Klijent.KorisnickoIme,
                    Odgovorio = k.Odgovoren,
                    Prihvatio = k.Prihvacen


                }).ToList()
            };
            return View(model);
        }
        public IActionResult KupovinaKnjigaPrikaz(string pretragaString)
        {
            if (string.IsNullOrEmpty(pretragaString))
            {
                KupovinaKnjigaPrikazVM model = new KupovinaKnjigaPrikazVM
                {
                    BrojProdanih = _db.KupovinaKnjiga.Count(),

                    rows = _db.KupovinaKnjiga.Select(k => new KupovinaKnjigaPrikazVM.Row
                    {

                        Knjiga = k.EKnjiga.Naziv,
                        Cijena = k.EKnjiga.Cijena,
                        Ocjena = _db.KlijentKnjigaOcijene.Where(s => s.EKnjigaID == k.EKnjigaID).Average(s => s.Ocjena),
                        Datum = k.DatumKupovine,
                        Kupac = k.Klijent.Ime + " " + k.Klijent.Prezime,


                    }).ToList()
                };
                return View(model);
            }
            else
            {
                KupovinaKnjigaPrikazVM model = new KupovinaKnjigaPrikazVM
                {
                    BrojProdanih = _db.KupovinaKnjiga.Count(),

                    rows = _db.KupovinaKnjiga.Select(k => new KupovinaKnjigaPrikazVM.Row
                    {

                        Knjiga = k.EKnjiga.Naziv,
                        Cijena = k.EKnjiga.Cijena,
                        Ocjena = _db.KlijentKnjigaOcijene.Where(s => s.EKnjigaID == k.EKnjigaID && s.KlijentID == k.KlijentID).Select(s => s.Ocjena).FirstOrDefault(),
                        Datum = k.DatumKupovine,
                        Kupac = k.Klijent.Ime + " " + k.Klijent.Prezime,



                    }).Where(k => k.Knjiga.ToLower().Contains(pretragaString.ToLower())).ToList()
                };
                return View(model);
            }
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
                    Administrator = k.Administrator.KorisnickoIme,
                    Odgovorio = k.Odgovoren,
                    Prihvatio = k.Prihvacen


                }).ToList()
            };
            return View(model);
        }
        public IActionResult Prihvaceno(int PrijedlogID)
        {
            PrijedlogKnjiga x = _db.PrijedlogKnjiga.Find(PrijedlogID);
            x.Prihvacen = true;
            x.Odgovoren = true;
            _db.SaveChanges();
            return RedirectToAction("PrijedlogKnjigePrikaz");
        }
        public IActionResult Odbijen(int PrijedlogID)
        {
            PrijedlogKnjiga x = _db.PrijedlogKnjiga.Find(PrijedlogID);
            x.Prihvacen = false;
            x.Odgovoren = true;
            _db.SaveChanges();
            return RedirectToAction("PrijedlogKnjigePrikaz");
        }
        public IActionResult Komentari(int KnjigaID)
        {
            KomentariVM model = new KomentariVM
            {
                KnjigaID = KnjigaID,
                Knjiga=_db.Komentari.Where(k=>k.EKnjigaID==KnjigaID).Select(k=>k.EKnjiga.Naziv).FirstOrDefault(),
                rows = _db.Komentari.Where(k => k.EKnjigaID == KnjigaID).Select(s => new KomentariVM.Row
                {
                    Komentar = s.komentar,
                    Korisnik = s.Klijent.KorisnickoIme,
                    Datum=s.DatumKomentara,
                    KomentarID=s.KomentarId
                }).ToList()

            };
           
            return View(model);
        }
    }
}
