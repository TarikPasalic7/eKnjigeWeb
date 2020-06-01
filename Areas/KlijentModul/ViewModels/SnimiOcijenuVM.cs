using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace eKnjige.VewModels
{
    public class SnimiOcijenuVM
    {
        public int KnjigaId { get; set; }

        public int Ocijenavrijednost { get; set; }

        public float prosijek { get; set; }

        public List<SelectListItem> Ocijena { get; set; }

    }
}
