using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eKnjige.Models
{
    public class Komentar
    {
        public int KomentarId { get; set; }

        public string komentar  { get; set; }

       public  DateTime DatumKomentara { get; set; }

        [ForeignKey("Klijent")]
        public int KlijentID { get; set; }
        public Klijent Klijent { get; set; }

        [ForeignKey("EKnjiga")]
        public int EKnjigaID { get; set; }
        public EKnjiga EKnjiga { get; set; }

    }
}
