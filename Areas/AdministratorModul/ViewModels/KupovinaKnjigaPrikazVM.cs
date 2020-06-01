using System;
using System.Collections.Generic;

namespace eKnjige.Areas.AdministratorModul.ViewModels
{
    public class KupovinaKnjigaPrikazVM
    {
        public List<Row> rows { get; set; }
        public int BrojProdanih { get; set; }
        public string NajprodavanijaKnjiga { get; set; }
        public class Row
        {
            public string Knjiga { get; set; }
            public float Cijena { get; set; }
          
            public float Ocjena { get; set; }
            public DateTime Datum { get; set; }
            public string Kupac { get; set; }
            public string BrojRacuna { get; set; }
            public int BrojProdanihPoNazivu { get; set; }
            
        }

    }
}
