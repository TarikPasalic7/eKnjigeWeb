using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eKnjige.Models
{
    public class EKnjigaKategorija
    {
        [Key]
        public int EKnjigaKategorijaID { get; set; }


        [ForeignKey("EKnjiga")]
        public int EKnjigaID { get; set; }
        public EKnjiga Eknjiga { get; set; }

        [ForeignKey("Kategorija")]
        public int KategorijaID { get; set; }
        public Kategorija Kategorija { get; set; }
    }
}
