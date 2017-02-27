namespace TestDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FM : DbMigration
    {
        public override void Up()
        {
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
                    })
                .PrimaryKey(t => t.PlayerId);
            
            CreateTable(
                "dbo.Ships",
                c => new
                    {
                        ShipId = c.Guid(nullable: false),
                        ShipName = c.String(nullable: false, maxLength: 50),
                        AttackValue = c.Int(nullable: false),
                        DefenseValue = c.Int(nullable: false),
                        Armor = c.Int(nullable: false),
                        Health = c.Int(nullable: false),
                        GroundTroopCapacity = c.Int(nullable: false),
                        PeopleToOperate = c.Int(nullable: false),
                        Speed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShipId);
            
            CreateTable(
                "dbo.ShipPlayers",
                c => new
                    {
                        Ship_ShipId = c.Guid(nullable: false),
                        Player_PlayerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ship_ShipId, t.Player_PlayerId })
                .ForeignKey("dbo.Ships", t => t.Ship_ShipId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.Player_PlayerId, cascadeDelete: true)
                .Index(t => t.Ship_ShipId)
                .Index(t => t.Player_PlayerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShipPlayers", "Player_PlayerId", "dbo.Players");
            DropForeignKey("dbo.ShipPlayers", "Ship_ShipId", "dbo.Ships");
            DropIndex("dbo.ShipPlayers", new[] { "Player_PlayerId" });
            DropIndex("dbo.ShipPlayers", new[] { "Ship_ShipId" });
            DropTable("dbo.ShipPlayers");
            DropTable("dbo.Ships");
            DropTable("dbo.Players");
        }
    }
}
