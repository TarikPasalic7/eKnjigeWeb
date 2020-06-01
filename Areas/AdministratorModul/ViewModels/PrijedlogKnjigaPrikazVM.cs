using System;
using System.Collections.Generic;

namespace eKnjige.Areas.AdministratorModul.ViewModels
{
    public class PrijedlogKnjigaPrikazVM
    {
        public List<Row> rows { get; set; }
        public class Row
        {
            public int PrijedlogKnjigeID { get; set; }

            public DateTime Datum { get; set; }

            public string Naziv { get; set; }
            public string Administrator { get; set; }

            public string Klijent { get; set; }
            public bool Odgovorio { get; set; }
            public bool Prihvatio { get; set; }
        }
    }
}
