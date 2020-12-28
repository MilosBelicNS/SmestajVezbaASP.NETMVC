
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SortFiltPagVezba.Models
{
    public class Rezervacija
    {
      
        public int Id { get; set; }
        [Required]
        [Display(Name = "Ime i prezime")]
        [StringLength(50,MinimumLength = 3, ErrorMessage ="Morate uneti najmanje 3 karatera!")]
        public string ImePrezime { get; set; }


        [Required]
        [DataType(DataType.Date)]   
        [CurrentDate(ErrorMessage ="Morate uneti buduce vreme!")] 
        [Display(Name = "Datum pocetka")]   
        public DateTime DatumPocetka { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Datum kraja")]
        [CurrentDate(ErrorMessage = "Morate uneti buduce vreme!")]
        public DateTime DatumKraja { get; set; }


        [Display(Name = "Otkazana?")]
        public bool Otkazana { get; set; }


        [ForeignKey("Soba")]
        [Display(Name = "Id sobe")]
        public int SobaId { get; set; }
        public Soba Soba { get; set; }




    

    }
}