using eKnjige.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eKnjige.Areas.AdministratorModul.ViewModels
{
    public class KnjigeDodajVM
    {
        [Required(ErrorMessage ="Naziv je obavezan")]
        public string Naziv { get; set; }
        public IFormFile SlikaByte { get; set; }
        public IFormFile PdfByte { get; set; }
        public IFormFile Mp3Byte { get; set; }
        [Required(ErrorMessage = "Ocjena je obavezna")]
        public float Cijena { get; set; }
        public string Opis { get; set; }
        [Display(Name = "Autori"), Required(ErrorMessage = "Autor je obavezan")]
        public int[] AutoriID { get; set; }
        public List<SelectListItem> Autori { get; set; }
        [Display(Name = "Kategorije"), Required(ErrorMessage = "Kategorija je obavezna")]
        public int[] KategorijeID { get; set; }
        public List<SelectListItem> Kategorije { get; set; }
        [Display(Name = "Tipovi"), Required(ErrorMessage = "Tip je obavezan")]
        public int[] TipoviId { get; set; }
        public List<SelectListItem> Tipovi { get; set; }
    }
}
