using eKnjige.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eKnjige.Areas.KlijentModul.ViewModels
{
    public class KnjigeKupovinaVM
    {
        public List<Row> rows { get; set; }

     
        public class Row
        {

            public int KnjigaID { get; set; }
            public string Naziv { get; set; }

            public float Cijena { get; set; }

            public float Ocjena { get; set; }

            public string Slika { get; set; }
        }
      

        public List<Kategorija> kategorije { get; set; }
        public List<TipFajla> tip { get; set; }

        public string Trazi { get; set; }


    }

  
}
