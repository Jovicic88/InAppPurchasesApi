﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InAppPurchasesApi.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PurchasesIAPEntities : DbContext
    {
        public PurchasesIAPEntities()
            : base("name=PurchasesIAPEntities")
        {
            Configuration.ProxyCreationEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGame> UserGames { get; set; }
        public virtual DbSet<UserWeapon> UserWeapons { get; set; }
        public virtual DbSet<Gun> Guns { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<UserMap> UserMaps { get; set; }
    }
}
