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
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MyGame.DB.MyGameDBContext context)
        {
            // Populating the databases with the Races
            #region Races
            List<Races> races = new List<Races>()
            {
                new Races() {RaceId = Guid.Parse("1a4ba370-cb02-49d2-ab72-b1ad019943dc"), RaceName = "Raptors", Description=" A lizardlike species bent on universal domination. Raptors multiply like no other and usually overcome their opponents with sheer numbers. With frightening technology that is based on a mix of biology and machine they instil fear in their enemies", RacialTraits="+15% Population growth, -15% Ship construction cost/time"},
                new Races() {RaceId = Guid.Parse("d401dbb4-f470-47c8-9075-8cfed7665e8c"), RaceName = "Centians", Description=" Natural galactic leaders, respected and adored throughout the universe for their benevolent nature and kind guidance. But even kindness has it’s limits", RacialTraits="+10% Galaxy guarding forces, +3% generated resources for holding galactic center"},
                new Races() {RaceId = Guid.Parse("32f4f4fc-6637-4c65-9b8a-33576f7e4c27"), RaceName = "Mixxels", Description="Having entered stage three and harnessing the power of galaxies, they are able to slip in and out of this dimension, cloaking their movement and transgressing distances with incredible speed. Neither friendly nor ruthless they maintain their own independence from their galactic neighbours.", RacialTraits="+15% Attack speed,  Undetectable attacks"},
                new Races() {RaceId = Guid.Parse("b1140d07-c9cb-4e86-814d-aef827cabc72"), RaceName = "Humans", Description="Having finally evolved past their own petty differences, humans took to the stars in an attempt to spread the teachings of lord hubbard throughout the galaxy, Clearly the craziest of all the species you never know what will happen whilst fighting a human", RacialTraits="3x the random factor in battle, -5% troop construction time"},
                new Races() {RaceId = Guid.Parse("42f53543-710a-4b74-af37-08fbebae8ca3"), RaceName = "Rogans", Description="The pinnacle of aggression. Rogans know nothing but war and thrive within in. It’s only during war times rogans find themselves being the best they can be, and as such it’s recommended to interact with them as little as possible",RacialTraits="+10% resource collection during war, -10% training/building cost and time during war"},
                new Races() {RaceId = Guid.Parse("5bcdbfdc-3a14-4743-9e96-c7b6b59cc267"), RaceName = "Fenxians", Description="Traders, through and through. Making a deal with a fenxian is almost always going to be the most lucrative thing for you, but expect them to get an even better deal for themselves",RacialTraits="No fee on the marketplace, Double the amount of daily marketplace deals."}
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
                new Buildings() {BuildingId = Guid.NewGuid(), BuildingName = "Residental",Description="Builds additional residential areas. Raises population and birthrates"},
                new Buildings() {BuildingId = Guid.NewGuid(), BuildingName = "Bank",Description="Raises the daily income of credits"},
                new Buildings() {BuildingId = Guid.NewGuid(), BuildingName = "Barracks",Description="Lowers the cost and time required to train groundtroops "},
                new Buildings() {BuildingId = Guid.NewGuid(), BuildingName = "Hangar",Description="Lowers the cost and time required to construct spaceships"},
                new Buildings() {BuildingId = Guid.NewGuid(), BuildingName = "Universitiy",Description="Increases the efficiency of research"},
                new Buildings() {BuildingId = Guid.NewGuid(), BuildingName = "Technological institute",Description="Provides free daily points to invest in research"},
                new Buildings() {BuildingId = Guid.NewGuid(), BuildingName = "Fort",Description="Decreases the training time required to train groundtroops and lowers the cost of defensive specialists"},
                new Buildings() {BuildingId = Guid.NewGuid(), BuildingName = "Space Station",Description="Increases the efficiency of your orbital defense"},
                new Buildings() {BuildingId = Guid.NewGuid(), BuildingName = "Drill",Description="Produces oil"},
                new Buildings() {BuildingId = Guid.NewGuid(), BuildingName = "Mine",Description="Produces Minerals"},
                new Buildings() {BuildingId = Guid.NewGuid(), BuildingName = "Power Plant",Description="Produces electricity"},
                new Buildings() {BuildingId = Guid.NewGuid(), BuildingName = "War mongers guild",Description="Decreases the training cost and time to train offensive specialists"}
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
            // Populating the database with Locationsq
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
                new Terrain() {TerrainId = Guid.NewGuid(), TerrainType = "Housing", TerrainDescription="A planet with an abundance of water, allowing for high population"},
                new Terrain() {TerrainId = Guid.NewGuid(), TerrainType = "Mineral", TerrainDescription="A planet with a lot of mineral deposits"},
                new Terrain() {TerrainId = Guid.NewGuid(), TerrainType = "Oil", TerrainDescription="A planet with a long biological history, resulting in vast deposits of biofuel"},
                new Terrain() {TerrainId = Guid.NewGuid(), TerrainType = "Nagrata", TerrainDescription="A planet that has existed almost since the formation of the universe, allowing for the mining of Nagrata"},
                new Terrain() {TerrainId = Guid.NewGuid(), TerrainType = "Special Credits", TerrainDescription="A planet already inhabited by an ancient grounddwelling alien race, allowing you to trade with them for special credits"},
                new Terrain() {TerrainId = Guid.NewGuid(), TerrainType = "Gas", TerrainDescription="A planet abundant of natural gas"},
                new Terrain() {TerrainId = Guid.NewGuid(), TerrainType = "Starter", TerrainDescription="A good starting planet, a bit of everything"},

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
                new Planets() {PlanetId = Guid.Parse("c6e51fb5-57fc-4b9e-ab99-cab987d1e763"), PlanetName = GeneratePlanetName(),TerrainRefId = terrain[0].TerrainId},
                new Planets() {PlanetId = Guid.Parse("7b82b99e-2ceb-473c-bb3b-d94e0acf1e27"), PlanetName = GeneratePlanetName(), TerrainRefId = terrain[1].TerrainId},
                new Planets() {PlanetId = Guid.Parse("6238b082-8b09-442a-80ac-22e2ca083881"), PlanetName = GeneratePlanetName(),TerrainRefId = terrain[2].TerrainId}
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
                new Research() {ResearchId = Guid.Parse("b0d657e1-8dbb-4c0f-89b5-6fc38d85739b"), ResearchName = "Economical"},
                new Research() {ResearchId = Guid.Parse("42a5a811-e13a-4ef4-9342-5025328d0775"), ResearchName = "Military"},
                new Research() {ResearchId = Guid.Parse("edf9c72e-c73a-4f95-b083-8b7805b829ab"), ResearchName = "Flight speed"}
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