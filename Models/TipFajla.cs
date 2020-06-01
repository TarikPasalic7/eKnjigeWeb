using System.ComponentModel.DataAnnotations;

namespace eKnjige.Models
{
    public class TipFajla
    {
        [Key]
        public int TipFajlaID { get; set; }
        public string Naziv { get; set; }
    }
}
