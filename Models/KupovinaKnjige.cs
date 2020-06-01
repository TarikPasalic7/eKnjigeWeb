using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eKnjige.Models
{
    public class KupovinaKnjige
    {
        [Key]
        public int KupovinaKnjigeID { get; set; }
        public DateTime DatumKupovine { get; set; }

        [ForeignKey("Klijent")]

        public int KlijentID { get; set; }

        public Klijent Klijent { get; set; }

        [ForeignKey("EKnjiga")]

        public int EKnjigaID { get; set; }

        public EKnjiga EKnjiga { get; set; }

       
    }
}
