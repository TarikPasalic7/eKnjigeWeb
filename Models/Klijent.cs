using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eKnjige.Models
{
    public class Klijent
    {
        [Key]
        public int KlijentID { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }

        public string Email { get; set; }

        public string Jmbg { get; set; }

        public DateTime DatumRodjenja { get; set; }

        [ForeignKey("Spol")]

        public int SpolID { get; set; }

        public Spol Spol { get; set; }

        [ForeignKey("Grad")]

        public int GradID { get; set; }

        public Grad Grad { get; set; }

       

    }
}
