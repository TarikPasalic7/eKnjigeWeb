using System.ComponentModel.DataAnnotations;

namespace eKnjige.Models
{
    public class Spol
    {

        [Key]
        public int SpolID { get; set; }
        public string Tip { get; set; }
    }
}
