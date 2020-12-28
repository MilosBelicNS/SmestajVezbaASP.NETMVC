using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SortFiltPagVezba.Models
{
    public class SmestajDbContext : DbContext
    {
        public DbSet<Soba> Sobe { get; set; }
        public DbSet<Smestaj> Smestaji { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }

        public SmestajDbContext() : base("name=Smestaj")
        {

        }
    }
}