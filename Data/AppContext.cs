using eKnjige.Models;
using Microsoft.EntityFrameworkCore;


namespace eKnjige.Data
{
    public class AppContext : DbContext
    {


        public AppContext(DbContextOptions<AppContext> options) : base(options)
        { }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PrijedlogKnjiga>()

                   .HasOne(pt => pt.Klijent)

                  .WithMany()

                 .HasForeignKey(pt => pt.KlijentID)

                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KlijentKnjigaOcijena>()
                .HasOne(p => p.Klijent).WithMany().HasForeignKey(p => p.KlijentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KupovinaKnjige>()
                .HasOne(p => p.Klijent).WithMany().HasForeignKey(p => p.KlijentID)
                .OnDelete(DeleteBehavior.Restrict);

        }


        public DbSet<Administrator> Administratori { get; set; }
        public DbSet<Autor> Autori { get; set; }

        public DbSet<Drzava> Drzave { get; set; }
        public DbSet<EKnjiga> EKnjige { get; set; }
        public DbSet<EKnjigaKategorija> EKnjigaKategorije { get; set; }
        public DbSet<EKnjigaTip> EKnjigaTipovi { get; set; }
        public DbSet<EKnjigeAutor> EKnjigaAutori { get; set; }
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }
        public DbSet<Klijent> Klijenti { get; set; }
        public DbSet<KlijentKnjigaOcijena> KlijentKnjigaOcijene { get; set; }
     
        public DbSet<KupovinaKnjige> KupovinaKnjiga { get; set; }
        public DbSet<PrijedlogKnjiga> PrijedlogKnjiga { get; set; }
        public DbSet<Spol> Spol { get; set; }
        public DbSet<TipFajla> TipFajlova { get; set; }

        public DbSet<Komentar> Komentari { get; set; }

      
    }
}
