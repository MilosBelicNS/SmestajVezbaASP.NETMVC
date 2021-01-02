namespace SortFiltPagVezba.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SortFiltPagVezba.Models.SmestajDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SortFiltPagVezba.Models.SmestajDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            Models.Smestaj smestaj1 = new Models.Smestaj()
            {
                Id = 1,
                Naziv = "Pendula",
                Opis = "3 zvezdice",
                Adresa = "Hajducka 45",
                Ocena = 3.55m,


            };
            
            //ubacujemo smestaj u tabelu
            context.Smestaji.AddOrUpdate(smestaj1);
            context.SaveChanges();

            Models.Soba soba1 = new Models.Soba()
            {
                Id = 1,
                BrojSobe = 1,
                BrojKreveta = 3,
                CenaNoc = 4000,
                SmestajId = 1

            };
            Models.Soba soba2 = new Models.Soba()
            {
                Id = 2,
                BrojSobe = 2,
                BrojKreveta = 2,
                CenaNoc = 2500,
                SmestajId = 1

            };
            Models.Soba soba3 = new Models.Soba()
            {
                Id = 3,
                BrojSobe = 3,
                BrojKreveta = 5,
                CenaNoc = 5000,
                SmestajId = 1

            };
            Models.Soba soba4 = new Models.Soba()
            {
                Id = 4,
                BrojSobe = 4,
                BrojKreveta = 1,
                CenaNoc = 2000,
                SmestajId = 1

            };
            context.Sobe.AddOrUpdate(soba1, soba2, soba3, soba4);
            context.SaveChanges();

            Models.Smestaj smestaj1M = context.Smestaji.FirstOrDefault(m => m.Id == 1);
            

            Models.Soba soba1M = context.Sobe.FirstOrDefault(m => m.Id == 1);
            Models.Soba soba2M = context.Sobe.FirstOrDefault(m => m.Id == 2);
            Models.Soba soba3M = context.Sobe.FirstOrDefault(m => m.Id == 3);
            Models.Soba soba4M = context.Sobe.FirstOrDefault(m => m.Id == 4);

            
            

            Models.Rezervacija rezervacija1 = new Models.Rezervacija()
            {
                Id = 1,
                ImePrezime = "Milos Belic",
                DatumPocetka = new DateTime(2021,01,28).Date,
                DatumKraja = new DateTime(2021,02,02).Date,
                SobaId = 4
            };
            context.Rezervacije.AddOrUpdate(rezervacija1);
            context.SaveChanges();

        }
    }
}
