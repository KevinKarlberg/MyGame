namespace MyGame.DB.Migrations
{
    using DB.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyGame.DB.MyGameDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MyGame.DB.MyGameDBContext context)
        {
            // Populating the databases with the Races
            #region Races
            List<Races> races = new List<Races>()
            {
                new Races() {RaceId = Guid.NewGuid(), RaceName = "Humans"},
                new Races() {RaceId = Guid.NewGuid(), RaceName = "Centians"}
            };
            foreach (var item in races)
            {
                context.Races.AddOrUpdate(x => x.RaceId, item);
            }
            #endregion
            // Populating the database with Buildings
            #region Buildings
            List<Buildings> buildings = new List<Buildings>()
            {
                new Buildings() {BuildingId = Guid.NewGuid(), BuildingName = "Residental"},
                new Buildings() {BuildingId = Guid.NewGuid(), BuildingName = "Banks"},
                new Buildings() {BuildingId = Guid.NewGuid(), BuildingName = "Barracks"}
            };
            foreach (var item in buildings)
            {
                context.Buildings.AddOrUpdate(x => x.BuildingId, item);
            }
            #endregion
            // Populating the database with Troops
            #region Troops
            List<Troops> troops = new List<Troops>()
            {
                new Troops() {TroopID = Guid.NewGuid(), TroopName = "Lvl1Fighter"},
                new Troops() {TroopID = Guid.NewGuid(), TroopName = "Lvl2Fighter"},
                new Troops() {TroopID = Guid.NewGuid(), TroopName = "Lvl3Fighter"}
            };
            foreach (var item in troops)
            {
                context.Troops.AddOrUpdate(x => x.TroopID, item);
            }
            #endregion
            // Populating the database with Locations
            #region Location
            List<Location> locations = new List<Location>()
            {
                new Location() {LocationId = Guid.NewGuid(), GalaxyNumber = 1, LocalCluster = 1, SystemNumber = 1},
                new Location() {LocationId = Guid.NewGuid(), GalaxyNumber = 1, LocalCluster = 1, SystemNumber = 2},
                new Location() {LocationId = Guid.NewGuid(), GalaxyNumber = 1, LocalCluster = 1, SystemNumber = 3}
            };
            foreach (var item in locations)
            {
                context.Locations.AddOrUpdate(x => x.LocationId, item);
            }
            #endregion
            // Populating the database with Terrains
            #region Terrains
            List<Terrain> terrain = new List<Terrain>()
            {
                new Terrain() {TerrainId = Guid.NewGuid(), TerrainType = "Water", TerrainDescription="A planet with a lot of liquid water present"},
                new Terrain() {TerrainId = Guid.NewGuid(), TerrainType = "Mineral", TerrainDescription="A planet with a lot of mineral deposits"},
                new Terrain() {TerrainId = Guid.NewGuid(), TerrainType = "Gas", TerrainDescription="A planet abundant of natural gas"}
            };
            foreach (var item in terrain)
            {
                context.Terrain.AddOrUpdate(x => x.TerrainId, item);
            }
            #endregion
            // Populating the database with Planets
            #region Planets
            List<Planets> planets = new List<Planets>()
            {
                new Planets() {PlanetId = Guid.NewGuid(), PlanetName = GeneratePlanetName(),TerrainRefId = terrain[0].TerrainId},
                new Planets() {PlanetId = Guid.NewGuid(), PlanetName = GeneratePlanetName(), TerrainRefId = terrain[1].TerrainId},
                new Planets() {PlanetId = Guid.NewGuid(), PlanetName = GeneratePlanetName(),TerrainRefId = terrain[2].TerrainId}
            };
            foreach (var item in planets)
            {
                context.Planets.AddOrUpdate(x => x.PlanetId, item);
            }
            #endregion
            // Populating the database with Ships
            #region Ships
            List<Ships> ships = new List<Ships>()
            {
                new Ships() {ShipId = Guid.NewGuid(), ShipName = "Lvl1Ship"},
                new Ships() {ShipId = Guid.NewGuid(), ShipName = "Lvl2Ship"},
                new Ships() {ShipId = Guid.NewGuid(), ShipName = "Lvl3Ship"}
            };
            foreach (var item in ships)
            {
                context.Ships.AddOrUpdate(x => x.ShipId, item);
            }
            #endregion
            // Populating the database with Research
            #region Research
            List<Research> research = new List<Research>()
            {
                new Research() {ResearchId = Guid.NewGuid(), ResearchName = "Economical"},
                new Research() {ResearchId = Guid.NewGuid(), ResearchName = "Military"},
                new Research() {ResearchId = Guid.NewGuid(), ResearchName = "Flight speed"}
            };
            foreach (var item in research)
            {
                context.Research.AddOrUpdate(x => x.ResearchId, item);
            }
            #endregion
            // Populating the database with Resources
            #region Resources
            List<Resources> resources = new List<Resources>()
            {
                new Resources() {ResourceId = Guid.NewGuid(), ResourceName = "Residental"},
                new Resources() {ResourceId = Guid.NewGuid(), ResourceName = "Banks"},
                new Resources() {ResourceId = Guid.NewGuid(), ResourceName = "Barracks"}
            };
            foreach (var item in resources)
            {
                context.Resources.AddOrUpdate(x => x.ResourceId, item);
            }
            #endregion
            //Populating the database with Players
            #region Players
            List<Players> players = new List<Players>()
            {
                new Players() {PlayerId = Guid.NewGuid(), EmpireName = "Water", ElectricityLevel=5, EmpireStrength= 2,Level=1, LocationRefId = locations[0].LocationId, RaceRefId = races[0].RaceId, TradeBalance = 0, PlayerAccountId = Guid.NewGuid() },
                new Players() {PlayerId = Guid.NewGuid(), EmpireName = "Mineral", ElectricityAvailable=5, EmpireStrength= 2,Level=1, LocationRefId = locations[1].LocationId, RaceRefId = races[0].RaceId, TradeBalance = 0, PlayerAccountId = Guid.NewGuid()},
                new Players() {PlayerId = Guid.NewGuid(), EmpireName = "Gas", ElectricityAvailable=5, EmpireStrength= 2,Level=1, LocationRefId = locations[2].LocationId, RaceRefId = races[0].RaceId, TradeBalance = 0, PlayerAccountId = Guid.NewGuid()}
            };
            foreach (var item in players)
            {
                context.Players.AddOrUpdate(x => x.PlayerId, item);
            }
            #endregion
        }
        private string GeneratePlanetName()
        {
            string[] prefix = new string[] { "Proxima", "Habilita", "Oxima", "Glaxico" };
            string[] suffix = new string[] { "Centaury", "Neo", "Supra", "Noxius" };

            Random rnd = new Random();

            return $"{prefix[rnd.Next(0, prefix.Length)]} {suffix[rnd.Next(0, suffix.Length)]}";
        }
    }
}
