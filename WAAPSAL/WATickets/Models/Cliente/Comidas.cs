namespace WATickets.Models.Cliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comidas
    {
        public int id { get; set; }

        public int? idDia { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }
 
        public byte[] Foto { get; set; }


        public int? Calorias { get; set; }
    }
}
