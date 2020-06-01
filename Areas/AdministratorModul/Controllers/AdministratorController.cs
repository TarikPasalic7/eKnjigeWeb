
using eKnjige.Areas.AdministratorModul.ViewModels;
using eKnjige.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace eKnjige.Controllers
{
    [Area("AdministratorModul")]
    public class AdministratorController : Controller
    {
        private readonly AppContext _db;
        public AdministratorController(AppContext db)
        {

            _db = db;
        }
        public IActionResult Index()
        {
            AdministratorIndexVM model = new AdministratorIndexVM();




            //Info o nalozima
            model.trenutnoKorisnika = _db.Klijenti.Count();
            model.administratora = _db.Administratori.Count();
            model.trenutnoKnjiga = _db.EKnjige.Count();
            model.trenutnoPrijedloga = _db.PrijedlogKnjiga.Count();
            model.prodanoKnjiga = _db.KupovinaKnjiga.Count();




            return View(model);
        }

    }
}
