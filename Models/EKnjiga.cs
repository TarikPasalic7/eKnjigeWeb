using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eKnjige.Models
{
    public class EKnjiga
    {
        [Key]
        public int EKnjigaID { get; set; }
        public string Naziv { get; set; }
        public float OcjenaKnjige { get; set; }
        public string Slika { get; set; }
        public float Cijena { get; set; }
        public string Mp3Path { get; set; }
        public string PdfPath { get; set; }
        public string Opis { get; set; }

        [ForeignKey("Administrator")]
        public int AdministratorID { get; set; }
        public Administrator Administrator { get; set; }
    }
}
