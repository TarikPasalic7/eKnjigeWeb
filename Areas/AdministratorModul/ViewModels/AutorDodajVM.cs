using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eKnjige.Areas.AdministratorModul.ViewModels
{
    public class AutorDodajVM
    {
        public int AutorID { get; set; }

        public string Ime { get; set; }
        public string Prezime { get; set; }

        public DateTime Godiste { get; set; }
    }
}
