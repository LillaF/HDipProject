using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using FinalMerchBuild.Models;

namespace FinalMerchBuild.DAL
{
    public class MerchBuildContext : DbContext
    {

        public MerchBuildContext() : base("MerchBuildContext")
        { }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Position> Bays { get; set; }
        //public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }

        public System.Data.Entity.DbSet<FinalMerchBuild.Models.Product> Products { get; set; }

    }
}