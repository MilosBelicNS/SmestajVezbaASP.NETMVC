using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SortFiltPagVezba.Models
{
    public class Smestaj
    {
      
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength =3,ErrorMessage ="Morate uneti najmanje 3 karaktera!")]
        [Display(Name = "Naziv smestaja")]
        public string Naziv { get; set; }
        [Required]
        public string Opis  { get; set; }
        [Required]
        public string Adresa { get; set; }
        [Required]
        [Range(1, 5)]
        public decimal Ocena { get; set; }

        public ICollection<Soba> Sobe { get; set; }
        public ICollection<Rezervacija> Rezervacije { get; set; }

    }
}