using Treater2.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Treater2.DAL
{
    public class TreatingContext : DbContext
    {
        public TreatingContext() : base("TreatingContext")
        {

        }
        public DbSet<TreaterTest> TreaterTests { get; set; }
        public DbSet<TreatingLine> TreatingLines { get; set; }
        public DbSet<AshPart> AshParts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ReleaseSheet> ReleaseSheets { get; set; }
        public DbSet<TreatingSpec> TreatingSpecs { get; set; }
        public DbSet<BarConfig> BarConfigs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}