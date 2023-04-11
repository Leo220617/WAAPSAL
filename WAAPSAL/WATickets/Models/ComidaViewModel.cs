using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WATickets.Models
{
    public class ComidaViewModel
    {
        public int id { get; set; }

        public int? idDia { get; set; }

      
        public string Descripcion { get; set; }

        public string Foto { get; set; }


        public int? Calorias { get; set; }
    }
}