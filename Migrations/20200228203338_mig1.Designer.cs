﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eKnjige.Data;

namespace eKnjige.Migrations
{
    [DbContext(typeof(Data.AppContext))]
    [Migration("20200228203338_mig1")]
    partial class mig1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eKnjige.Models.Administrator", b =>
                {
                    b.Property<int>("AdministratorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GradID")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KorisnickoIme")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lozinka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdministratorID");

                    b.HasIndex("GradID");

                    b.ToTable("Administratori");
                });

            modelBuilder.Entity("eKnjige.Models.Autor", b =>
                {
                    b.Property<int>("AutorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Godiste")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AutorID");

                    b.ToTable("Autori");
                });

            modelBuilder.Entity("eKnjige.Models.Drzava", b =>
                {
                    b.Property<int>("DrzavaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DrzavaID");

                    b.ToTable("Drzave");
                });

            modelBuilder.Entity("eKnjige.Models.EKnjiga", b =>
                {
                    b.Property<int>("EKnjigaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdministratorID")
                        .HasColumnType("int");

                    b.Property<float>("Cijena")
                        .HasColumnType("real");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("OcijenaKnjige")
                        .HasColumnType("real");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slika")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EKnjigaID");

                    b.HasIndex("AdministratorID");

                    b.ToTable("EKnjige");
                });

            modelBuilder.Entity("eKnjige.Models.EKnjigaKategorija", b =>
                {
                    b.Property<int>("EKnjigaKategorijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EKnjigaID")
                        .HasColumnType("int");

                    b.Property<int>("KategorijaID")
                        .HasColumnType("int");

                    b.HasKey("EKnjigaKategorijaID");

                    b.HasIndex("EKnjigaID");

                    b.HasIndex("KategorijaID");

                    b.ToTable("EKnjigaKategorije");
                });

            modelBuilder.Entity("eKnjige.Models.EKnjigaTip", b =>
                {
                    b.Property<int>("EKnjigaTipID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EKnjigaID")
                        .HasColumnType("int");

                    b.Property<int>("TipFajlaID")
                        .HasColumnType("int");

                    b.HasKey("EKnjigaTipID");

                    b.HasIndex("EKnjigaID");

                    b.HasIndex("TipFajlaID");

                    b.ToTable("EKnjigaTipovi");
                });

            modelBuilder.Entity("eKnjige.Models.EKnjigeAutor", b =>
                {
                    b.Property<int>("EKnjigeAutorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AutorID")
                        .HasColumnType("int");

                    b.Property<int>("EKnjigaID")
                        .HasColumnType("int");

                    b.HasKey("EKnjigeAutorID");

                    b.HasIndex("AutorID");

                    b.HasIndex("EKnjigaID");

                    b.ToTable("EKnjigaAutori");
                });

            modelBuilder.Entity("eKnjige.Models.Grad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DrzavaId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DrzavaId");

                    b.ToTable("Gradovi");
                });

            modelBuilder.Entity("eKnjige.Models.Kategorija", b =>
                {
                    b.Property<int>("KategorijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KategorijaID");

                    b.ToTable("Kategorije");
                });

            modelBuilder.Entity("eKnjige.Models.Klijent", b =>
                {
                    b.Property<int>("KlijentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumRodjenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GradID")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Jmbg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KorisnickoIme")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lozinka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpolID")
                        .HasColumnType("int");

                    b.HasKey("KlijentID");

                    b.HasIndex("GradID");

                    b.HasIndex("SpolID");

                    b.ToTable("Klijenti");
                });

            modelBuilder.Entity("eKnjige.Models.KlijentKnjigaOcijena", b =>
                {
                    b.Property<int>("KlijentKnjigaOcijenaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumOcijene")
                        .HasColumnType("datetime2");

                    b.Property<int>("EKnjigaID")
                        .HasColumnType("int");

                    b.Property<int>("KlijentID")
                        .HasColumnType("int");

                    b.Property<float>("Ocijena")
                        .HasColumnType("real");

                    b.HasKey("KlijentKnjigaOcijenaID");

                    b.HasIndex("EKnjigaID");

                    b.HasIndex("KlijentID");

                    b.ToTable("KlijentKnjigaOcijene");
                });

            modelBuilder.Entity("eKnjige.Models.KreditnaKartica", b =>
                {
                    b.Property<int>("KreditnaKarticaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrojKartice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CVCBroj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DatumIstekaKartice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KlijentID")
                        .HasColumnType("int");

                    b.HasKey("KreditnaKarticaID");

                    b.HasIndex("KlijentID");

                    b.ToTable("KreditneKartice");
                });

            modelBuilder.Entity("eKnjige.Models.KupovinaKnjige", b =>
                {
                    b.Property<int>("KupovinaKnjigeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumKupovine")
                        .HasColumnType("datetime2");

                    b.Property<int>("EKnjigaID")
                        .HasColumnType("int");

                    b.Property<int>("KlijentID")
                        .HasColumnType("int");

                    b.HasKey("KupovinaKnjigeID");

                    b.HasIndex("EKnjigaID");

                    b.HasIndex("KlijentID");

                    b.ToTable("KupovinaKnjiga");
                });

            modelBuilder.Entity("eKnjige.Models.PrijedlogKnjiga", b =>
                {
                    b.Property<int>("PrijedlogKnjigeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdministratorID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("KlijentID")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PrijedlogKnjigeID");

                    b.HasIndex("AdministratorID");

                    b.HasIndex("KlijentID");

                    b.ToTable("PrijedlogKnjiga");
                });

            modelBuilder.Entity("eKnjige.Models.Spol", b =>
                {
                    b.Property<int>("SpolID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tip")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpolID");

                    b.ToTable("Spol");
                });

            modelBuilder.Entity("eKnjige.Models.TipFajla", b =>
                {
                    b.Property<int>("TipFajlaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipFajlaID");

                    b.ToTable("TipFajlova");
                });

            modelBuilder.Entity("eKnjige.Models.Administrator", b =>
                {
                    b.HasOne("eKnjige.Models.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eKnjige.Models.EKnjiga", b =>
                {
                    b.HasOne("eKnjige.Models.Administrator", "Administrator")
                        .WithMany()
                        .HasForeignKey("AdministratorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eKnjige.Models.EKnjigaKategorija", b =>
                {
                    b.HasOne("eKnjige.Models.EKnjiga", "Eknjiga")
                        .WithMany()
                        .HasForeignKey("EKnjigaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eKnjige.Models.Kategorija", "Kategorija")
                        .WithMany()
                        .HasForeignKey("KategorijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eKnjige.Models.EKnjigaTip", b =>
                {
                    b.HasOne("eKnjige.Models.EKnjiga", "Eknjiga")
                        .WithMany()
                        .HasForeignKey("EKnjigaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eKnjige.Models.TipFajla", "Tipfajla")
                        .WithMany()
                        .HasForeignKey("TipFajlaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eKnjige.Models.EKnjigeAutor", b =>
                {
                    b.HasOne("eKnjige.Models.Autor", "Autor")
                        .WithMany()
                        .HasForeignKey("AutorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eKnjige.Models.EKnjiga", "EKnjiga")
                        .WithMany()
                        .HasForeignKey("EKnjigaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eKnjige.Models.Grad", b =>
                {
                    b.HasOne("eKnjige.Models.Drzava", "Drzava")
                        .WithMany()
                        .HasForeignKey("DrzavaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eKnjige.Models.Klijent", b =>
                {
                    b.HasOne("eKnjige.Models.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eKnjige.Models.Spol", "Spol")
                        .WithMany()
                        .HasForeignKey("SpolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eKnjige.Models.KlijentKnjigaOcijena", b =>
                {
                    b.HasOne("eKnjige.Models.EKnjiga", "Eknjiga")
                        .WithMany()
                        .HasForeignKey("EKnjigaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eKnjige.Models.Klijent", "Klijent")
                        .WithMany()
                        .HasForeignKey("KlijentID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("eKnjige.Models.KreditnaKartica", b =>
                {
                    b.HasOne("eKnjige.Models.Klijent", "Klijent")
                        .WithMany()
                        .HasForeignKey("KlijentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eKnjige.Models.KupovinaKnjige", b =>
                {
                    b.HasOne("eKnjige.Models.EKnjiga", "EKnjiga")
                        .WithMany()
                        .HasForeignKey("EKnjigaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eKnjige.Models.Klijent", "Klijent")
                        .WithMany()
                        .HasForeignKey("KlijentID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("eKnjige.Models.PrijedlogKnjiga", b =>
                {
                    b.HasOne("eKnjige.Models.Administrator", "Administrator")
                        .WithMany()
                        .HasForeignKey("AdministratorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eKnjige.Models.Klijent", "Klijent")
                        .WithMany()
                        .HasForeignKey("KlijentID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
