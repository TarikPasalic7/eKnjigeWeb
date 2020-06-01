using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eKnjige.Models
{
    public class KlijentKnjigaOcijena
    {
        [Key]
        public int KlijentKnjigaOcijenaID { get; set; }

        public DateTime DatumOcijene { get; set; }

        public float Ocjena { get; set; }

        [ForeignKey("Klijent")]

        public int KlijentID { get; set; }

        public Klijent Klijent { get; set; }

        [ForeignKey("EKnjiga")]

        public int EKnjigaID { get; set; }

        public EKnjiga Eknjiga { get; set; }

    }
}
