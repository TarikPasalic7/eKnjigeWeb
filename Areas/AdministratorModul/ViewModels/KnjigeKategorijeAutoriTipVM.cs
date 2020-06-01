using System.Collections.Generic;

namespace eKnjige.Areas.AdministratorModul.ViewModels
{
    public class KnjigeKategorijeAutoriTipVM
    {
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<Row> htmlrows { get; set; }
    
            public string Autor { get; set; }
        
    
            public string Kategorija { get; set; }
       
        
            public string TipFajla { get; set; }
        
        public string pretragaString { get; set; }
        public class Row
        {
            public int KnjigaID { get; set; }
            public string Naziv { get; set; }
            public string Slika { get; set; }
            public float Cijena { get; set; }
            public float OcijenaKnjige { get; set; }
            public string Autori { get; set; }
            public string Kategorije { get; set; }
            public string TipFajlova { get; set; }
        }
        
    }
}
