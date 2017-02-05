namespace MyGame.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        BuildingId = c.Guid(nullable: false),
                        BuildingName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.BuildingId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Guid(nullable: false),
                        GalaxyNumber = c.Int(nullable: false),
                        LocalCluster = c.Int(nullable: false),
                        SystemNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LocationId);
            
            CreateTable(
                "dbo.Planets",
                c => new
                    {
                        PlanetId = c.Guid(nullable: false),
                        PlanetName = c.String(nullable: false, maxLength: 50),
                        TerrainRefId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PlanetId)
                .ForeignKey("dbo.Terrains", t => t.TerrainRefId, cascadeDelete: true)
                .Index(t => t.TerrainRefId);
            
            CreateTable(
                "dbo.Terrains",
                c => new
                    {
                        TerrainId = c.Guid(nullable: false),
                        TerrainType = c.String(nullable: false, maxLength: 50),
                        TerrainDescription = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.TerrainId);
            
            CreateTable(
                "dbo.PlayerBuildings",
                c => new
                    {
                        PlayerId = c.Guid(nullable: false),
                        BuildingId = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.BuildingId })
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.BuildingId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Guid(nullable: false),
                        PlayerAccountId = c.Guid(nullable: false),
                        EmpireName = c.String(nullable: false, maxLength: 50),
                        TradeBalance = c.Int(nullable: false),
                        EmpireStrength = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        ElectricityAvailable = c.Int(nullable: false),
                        RaceRefId = c.Guid(nullable: false),
                        LocationRefId = c.Guid(nullable: false),
                        ElectricityLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerId)
                .ForeignKey("dbo.Locations", t => t.LocationRefId, cascadeDelete: true)
                .ForeignKey("dbo.Races", t => t.RaceRefId, cascadeDelete: true)
                .Index(t => t.RaceRefId)
                .Index(t => t.LocationRefId);
            
            CreateTable(
                "dbo.Races",
                c => new
                    {
                        RaceId = c.Guid(nullable: false),
                        RaceName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RaceId);
            
            CreateTable(
                "dbo.PlayerPlanets",
                c => new
                    {
                        PlayerId = c.Guid(nullable: false),
                        PlanetId = c.Guid(nullable: false),
                        CurrentSize = c.Int(nullable: false),
                        MaxSize = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.PlanetId })
                .ForeignKey("dbo.Planets", t => t.PlanetId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.PlanetId);
            
            CreateTable(
                "dbo.PlayerResearches",
                c => new
                    {
                        PlayerId = c.Guid(nullable: false),
                        ResearchId = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.ResearchId })
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Researches", t => t.ResearchId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.ResearchId);
            
            CreateTable(
                "dbo.Researches",
                c => new
                    {
                        ResearchId = c.Guid(nullable: false),
                        ResearchName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ResearchId);
            
            CreateTable(
                "dbo.PlayerResources",
                c => new
                    {
                        PlayerId = c.Guid(nullable: false),
                        ResourcesId = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Resource_ResourceId = c.Guid(),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.ResourcesId })
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Resources", t => t.Resource_ResourceId)
                .Index(t => t.PlayerId)
                .Index(t => t.Resource_ResourceId);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        ResourceId = c.Guid(nullable: false),
                        ResourceName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ResourceId);
            
            CreateTable(
                "dbo.PlayerShips",
                c => new
                    {
                        ShipId = c.Guid(nullable: false),
                        PlayerId = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShipId, t.PlayerId })
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Ships", t => t.ShipId, cascadeDelete: true)
                .Index(t => t.ShipId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Ships",
                c => new
                    {
                        ShipId = c.Guid(nullable: false),
                        ShipName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ShipId);
            
            CreateTable(
                "dbo.PlayerTroops",
                c => new
                    {
                        TroopId = c.Guid(nullable: false),
                        PlayerId = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TroopId, t.PlayerId })
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Troops", t => t.TroopId, cascadeDelete: true)
                .Index(t => t.TroopId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Troops",
                c => new
                    {
                        TroopID = c.Guid(nullable: false),
                        TroopName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TroopID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerTroops", "TroopId", "dbo.Troops");
            DropForeignKey("dbo.PlayerTroops", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerShips", "ShipId", "dbo.Ships");
            DropForeignKey("dbo.PlayerShips", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerResources", "Resource_ResourceId", "dbo.Resources");
            DropForeignKey("dbo.PlayerResources", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerResearches", "ResearchId", "dbo.Researches");
            DropForeignKey("dbo.PlayerResearches", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerPlanets", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerPlanets", "PlanetId", "dbo.Planets");
            DropForeignKey("dbo.PlayerBuildings", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Players", "RaceRefId", "dbo.Races");
            DropForeignKey("dbo.Players", "LocationRefId", "dbo.Locations");
            DropForeignKey("dbo.PlayerBuildings", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Planets", "TerrainRefId", "dbo.Terrains");
            DropIndex("dbo.PlayerTroops", new[] { "PlayerId" });
            DropIndex("dbo.PlayerTroops", new[] { "TroopId" });
            DropIndex("dbo.PlayerShips", new[] { "PlayerId" });
            DropIndex("dbo.PlayerShips", new[] { "ShipId" });
            DropIndex("dbo.PlayerResources", new[] { "Resource_ResourceId" });
            DropIndex("dbo.PlayerResources", new[] { "PlayerId" });
            DropIndex("dbo.PlayerResearches", new[] { "ResearchId" });
            DropIndex("dbo.PlayerResearches", new[] { "PlayerId" });
            DropIndex("dbo.PlayerPlanets", new[] { "PlanetId" });
            DropIndex("dbo.PlayerPlanets", new[] { "PlayerId" });
            DropIndex("dbo.Players", new[] { "LocationRefId" });
            DropIndex("dbo.Players", new[] { "RaceRefId" });
            DropIndex("dbo.PlayerBuildings", new[] { "BuildingId" });
            DropIndex("dbo.PlayerBuildings", new[] { "PlayerId" });
            DropIndex("dbo.Planets", new[] { "TerrainRefId" });
            DropTable("dbo.Troops");
            DropTable("dbo.PlayerTroops");
            DropTable("dbo.Ships");
            DropTable("dbo.PlayerShips");
            DropTable("dbo.Resources");
            DropTable("dbo.PlayerResources");
            DropTable("dbo.Researches");
            DropTable("dbo.PlayerResearches");
            DropTable("dbo.PlayerPlanets");
            DropTable("dbo.Races");
            DropTable("dbo.Players");
            DropTable("dbo.PlayerBuildings");
            DropTable("dbo.Terrains");
            DropTable("dbo.Planets");
            DropTable("dbo.Locations");
            DropTable("dbo.Buildings");
        }
    }
}
