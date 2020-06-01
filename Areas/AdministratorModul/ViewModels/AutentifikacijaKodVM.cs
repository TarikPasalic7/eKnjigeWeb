using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eKnjige.Areas.AdministratorModul.ViewModels
{
    public class AutentifikacijaKodVM
    {
        public int adminId { get; set; }
        public string kodGen { get; set; }
        public string kodKor { get; set; }
        public bool Poslan { get; set; }
    }
}
