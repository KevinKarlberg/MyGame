using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDb.Models;

namespace TestDb
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Ship> Ships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasMany<Ship>(s => s.Ships)
                .WithMany(p => p.Players)
                .Map(cs =>
                {
                    cs.MapLeftKey("PlayerRefId");
                    cs.MapRightKey("ShipRefId");
                    cs.ToTable("PlayerShip");
                });
        }
    }
}
