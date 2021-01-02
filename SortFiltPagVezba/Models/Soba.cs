
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SortFiltPagVezba.Models
{
    public class Soba
    {

        
        public int Id { get; set; }
        [Required]
        [Display(Name = "Broj sobe")]
        [Range(1, 100, ErrorMessage ="Mozete uneti vrednost od 1-100!")]
        public int BrojSobe { get; set; }


        [Required]
        [Display(Name = "Broj kreveta")]
        [Range(1, 6, ErrorMessage = "Soba moze imati najvise 6 kreveta!")]
        public int BrojKreveta { get; set; }


        [Required]
        [Display(Name = "Cena za noc")]
        [Range(1999, 10000, ErrorMessage="Cena sobe moze biti izmedju 2000 i 10000 dinara!")]
        public int CenaNoc { get; set; }

        [ForeignKey("Smestaj")]
        public int SmestajId { get; set; }
      
        public Smestaj Smestaj { get; set; }

        public ICollection<Rezervacija> Rezervacije { get; set; }
    }
}