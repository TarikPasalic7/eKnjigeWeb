using System;
using System.ComponentModel.DataAnnotations;

namespace eKnjige.Models
{
    public class Autor
    {
        [Key]
        public int AutorID { get; set; }

        public string Ime { get; set; }
        public string Prezime { get; set; }

        public DateTime Godiste { get; set; }
    }
}
