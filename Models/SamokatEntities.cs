using System.Data.Entity;

namespace SamokatAppClean
{
    public class SamokatEntities : DbContext
    {
        public SamokatEntities() : base("name=SamokatEntities") { }

        public virtual DbSet<clients> clients { get; set; }
        public virtual DbSet<couriers> couriers { get; set; }
        public virtual DbSet<transport> transport { get; set; }
        public virtual DbSet<orders> orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<clients>().ToTable("clients");
            modelBuilder.Entity<couriers>().ToTable("couriers");
            modelBuilder.Entity<transport>().ToTable("transport");
            modelBuilder.Entity<orders>().ToTable("orders");
        }
    }
}