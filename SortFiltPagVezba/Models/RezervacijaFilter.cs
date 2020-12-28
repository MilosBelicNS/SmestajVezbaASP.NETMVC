using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SortFiltPagVezba.Models
{
    public class RezervacijaFilter
    {

        [DataType(DataType.Date)]
        public DateTime? DatumPocetka { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DatumKraja { get; set; }
       
        public int? BrojSobe { get; set; }
       
    }
}