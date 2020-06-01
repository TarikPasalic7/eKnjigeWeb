using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eKnjige.Areas.AdministratorModul.ViewModels
{
    public class KorisniciUrediVM
    {
        public int KlijentID { get; set; }
        [Required(ErrorMessage = "Ime je obavezno")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Prezime je obavezno")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Korisnicko ime je obavezno")]
        public string KorisnickoIme { get; set; }
        public DateTime DatumRodjenja { get; set; }
        [Required(ErrorMessage = "Email je obavezan")]
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Jmbg { get; set; }
    
        public int SpolID { get; set; }
        public List<SelectListItem> Spol { get; set; }
        public int GradID { get; set; }
        public List<SelectListItem> Grad { get; set; }
    }
}
