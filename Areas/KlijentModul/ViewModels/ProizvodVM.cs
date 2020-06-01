using eKnjige.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eKnjige.Areas.KlijentModul.ViewModels
{
    public class ProizvodVM
    {
        public int KnjigaId { get; set; }

        public string Naslov { get; set; }

        public string Opis { get; set; }

        public string CardName { get; set; }

        public string Email { get; set; }

        public string StripeToken { get; set; }

      

        public float Cijena { get; set; }

        public float Ocjena { get; set; }

        public string komentar { get; set; }

        public List<Komentar> Komentari { get; set; }



        public string Slika { get; set; }
      
            
          

        public List<Row> rows { get; set; }
        public List<Row2> rows2 { get; set; }
        public List<Row3> rows3 { get; set; }

        public class Row
        {
                  public string Autor { get; set; }
                 
        }
        public class Row2
        {
            
            public string Kategorija { get; set; }
           
        }

        public class Row3
        {
           
            public string TipFajla { get; set; }
        }

    }

    
}
