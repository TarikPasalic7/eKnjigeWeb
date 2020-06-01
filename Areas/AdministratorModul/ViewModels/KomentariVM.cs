using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eKnjige.Areas.AdministratorModul.ViewModels
{
    public class KomentariVM
    {
        public int KnjigaID { get; set; }
        public string Knjiga { get; set; }
        public List<Row> rows { get; set; }
        public class Row
        {
            public int KomentarID { get; set; }
            public string Komentar { get; set; }
            public string Korisnik { get; set; }
            public DateTime Datum { get; set; }
          
        }
    }
}
