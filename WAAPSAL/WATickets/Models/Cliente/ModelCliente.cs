namespace WATickets.Models.Cliente
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelCliente : DbContext
    {
        public ModelCliente()
            : base("name=ModelCliente")
        {
        }

        public virtual DbSet<Comidas> Comidas { get; set; }
        public virtual DbSet<Dias> Dias { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SeguridadModulos> SeguridadModulos { get; set; }
        public virtual DbSet<SeguridadRolesModulos> SeguridadRolesModulos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comidas>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            

            modelBuilder.Entity<Dias>()
                .Property(e => e.Dia)
                .IsUnicode(false);

            modelBuilder.Entity<Dias>()
                .Property(e => e.Mes)
                .IsUnicode(false);

            modelBuilder.Entity<Dias>()
                .Property(e => e.Anno)
                .IsUnicode(false);

            modelBuilder.Entity<Dias>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.Clave)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .Property(e => e.NombreRol)
                .IsUnicode(false);

            modelBuilder.Entity<SeguridadModulos>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);
        }
    }
}
