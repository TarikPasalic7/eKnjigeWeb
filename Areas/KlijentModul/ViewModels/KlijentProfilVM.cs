using eKnjige.Models;
using System.Collections.Generic;
using X.PagedList;

namespace eKnjige.VewModels
{
    public class KlijentProfilVM
    {



        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Grad { get; set; }
        public string Spol { get; set; }
      public         IPagedList<KupovinaKnjige> pagelist { get; set; }
        public List<Row> rows { get; set; }


        public class Row
        {
            public int KnjigaId { get; set; }
            public string NazivKnjige { get; set; }
            public float ocijena { get; set; }

            public string AutorKnjige { get; set; }
            public string slika { get; set; }

            public string mp3 { get; set; }

            public string pdf { get; set; }
        }
    }


}
