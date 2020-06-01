using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eKnjige.Models
{
    public class Administrator
    {
        [Key]
        public int AdministratorID { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }

        public string Email { get; set; }

        [ForeignKey("Grad")]

        public int GradID { get; set; }

        public Grad Grad { get; set; }

    }
}
