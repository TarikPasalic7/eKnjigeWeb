using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eKnjige.Areas.KlijentModul.ViewModels
{
    public class AutentifikacijaKodVM
    {
       
            public int klijentId { get; set; }
            public string kodGen { get; set; }
            public string kodKor { get; set; }
            public bool Poslan { get; set; }
        
    }
}
