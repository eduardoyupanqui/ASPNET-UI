﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TrailersDeVideoJuegosEntities : DbContext
    {
        public TrailersDeVideoJuegosEntities()
            : base("name=TrailersDeVideoJuegosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Consola> Consola { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Imagenes> Imagenes { get; set; }
        public DbSet<Juego> Juego { get; set; }
    }
}
