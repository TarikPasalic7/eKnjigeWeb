using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eKnjige.Models
{
    public class EKnjigaTip
    {
        [Key]
        public int EKnjigaTipID { get; set; }

        [ForeignKey("EKnjiga")]
        public int EKnjigaID { get; set; }
        public EKnjiga Eknjiga { get; set; }

        [ForeignKey("TipFajla")]

        public int TipFajlaID { get; set; }
        public TipFajla Tipfajla { get; set; }

    }
}
