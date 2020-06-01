using eKnjige.Models;
using Microsoft.AspNetCore.Http;

namespace eKnjige.Helpers
{
    public static class Autentifikacija
    {

        private const string LogiraniKorisnik = "logirani_korisnik";
        private const string LogiraniAdministrator = "logirani_administrator";

        public static void Setlogiranikorisnik(this HttpContext context, Klijent klijent, bool snimiuCookie = false)
        {

            context.Session.SetObjectAsJson(LogiraniKorisnik, klijent);

        }

        public static Klijent getKorisnickiNalog(this HttpContext context)
        {
            Klijent klijent = context.Session.GetObjectFromJson<Klijent>(LogiraniKorisnik);
            return klijent;

        }
        public static void SetlogiraniAdministrator(this HttpContext context, Administrator administrator, bool snimiuCookie = false)
        {

            context.Session.SetObjectAsJson(LogiraniAdministrator, administrator);

        }

        public static Administrator getKorisnickiNalogAdministrator(this HttpContext context)
        {
            Administrator administrator = context.Session.GetObjectFromJson<Administrator>(LogiraniAdministrator);
            return administrator;

        }
    }
}
