using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eKnjige.Models
{
    public class PrijedlogKnjiga
    {

        [Key]

        public int PrijedlogKnjigeID { get; set; }

        public DateTime Datum { get; set; }
        public bool Prihvacen { get; set; }
        public bool Odgovoren { get; set; }
        public string Naziv { get; set; }

        [ForeignKey("Klijent")]

        public int KlijentID { get; set; }

        public Klijent Klijent { get; set; }

        [ForeignKey("Administrator")]

        public int AdministratorID { get; set; }

        public Administrator Administrator { get; set; }
    }
}
