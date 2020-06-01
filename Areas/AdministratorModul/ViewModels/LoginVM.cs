using System.ComponentModel.DataAnnotations;

namespace eKnjige.Areas.AdministratorModul.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Korisnicko ime je obavezno.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Lozinka je obavezna.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Zapamptipassword { get; set; }
    }
}
