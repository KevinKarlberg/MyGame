using MyGame.DB.DB.Models;
using MyGame.DB.DB.Models.Mailfunction;
using MyGame.DB.DB.Models.Market;
using MyGame.DB.DB.Models.ProtectionLevel;
using MyGame.DB.DB.Models.SystemChat;
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
        public DbSet<MissionTypes> MissionTypes { get; set; }
        public DbSet<Missions> Missions { get; set; }
        public DbSet<Buildings> Buildings { get; set; }
        public DbSet<Planets> Planets { get; set; }
        public DbSet<Location> Locations { get; set; }
        public virtual DbSet<RacesRacialTraits> RacesRacialTraits { get; set; }
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
        public DbSet<Mail> Mails { get; set; }
        public virtual DbSet<PlayerMail> PlayerMail { get; set; }
        public DbSet<Buying> Buying { get; set; }
        public DbSet<Selling> Selling { get; set; }
        public DbSet<MarketContent> MarketContent { get; set; }
        public virtual DbSet<PlayerMarket> PlayerMarket { get; set; }
        public virtual DbSet<PlayerProtectionLevel> PlayerProtectionLevel { get; set; }
        public DbSet<ProtectionLevel> ProtectionLevel { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Message> Message { get; set; }
    }
}
    