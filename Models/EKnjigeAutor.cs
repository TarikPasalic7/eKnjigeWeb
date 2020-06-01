using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eKnjige.Models
{
    public class EKnjigeAutor
    {
        [Key]
        public int EKnjigeAutorID { get; set; }

        [ForeignKey("Autor")]
        public int AutorID { get; set; }
        public Autor Autor { get; set; }

        [ForeignKey("EKnjiga")]
        public int EKnjigaID { get; set; }

        public EKnjiga EKnjiga { get; set; }
    }
}
