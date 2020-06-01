using System.ComponentModel.DataAnnotations;

namespace eKnjige.Models
{
    public class Drzava
    {

        [Key]
        public int DrzavaID { get; set; }
        public string Naziv { get; set; }
    }
}
