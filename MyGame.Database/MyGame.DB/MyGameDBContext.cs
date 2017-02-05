using MyGame.DB.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyGame.DB
{
    public class MyGameDBContext : DbContext
    {
        public MyGameDBContext() : base("name=DefaultConnection")
        {

        }
        public DbSet<Buildings> Buildings { get; set; }
        public DbSet<Planets> Planets { get; set; }
        public DbSet<Location> Locations { get; set; }
        public virtual DbSet<PlayerBuildings> PlayerBuildings { get; set; }
        public virtual DbSet<PlayerPlanets> PlayerPlanets { get; set; }

        public virtual DbSet<PlayerResearch> PlayerResearch { get; set; }
        public virtual DbSet<PlayerResources> PlayerResources { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<PlayerShips> PlayerShips { get; set; }
        public virtual DbSet<PlayerTroops> PlayerTroops { get; set; }
        public DbSet<Races> Races { get; set; }
        public DbSet<Research> Research { get; set; }
        public DbSet<Ships> Ships { get; set; }
        public DbSet<Terrain> Terrain { get; set; }
        public DbSet<Troops> Troops { get; set; }
        public DbSet<Resources> Resources { get; set; }
    }
}
    