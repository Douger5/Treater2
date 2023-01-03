namespace Treater2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AshPart",
                c => new
                    {
                        AshPartID = c.String(nullable: false, maxLength: 128),
                        ResinMix = c.String(),
                        TreatingSpecID = c.String(maxLength: 128),
                        Paper = c.String(),
                        Size = c.String(),
                    })
                .PrimaryKey(t => t.AshPartID)
                .ForeignKey("dbo.TreatingSpec", t => t.TreatingSpecID)
                .Index(t => t.TreatingSpecID);
            
            CreateTable(
                "dbo.TreatingSpec",
                c => new
                    {
                        TreatingSpecID = c.String(nullable: false, maxLength: 128),
                        FlowTarget = c.Double(nullable: false),
                        FlowMax = c.Double(nullable: false),
                        FlowMin = c.Double(nullable: false),
                        RCTarget = c.Double(nullable: false),
                        RCMax = c.Double(nullable: false),
                        RCMin = c.Double(nullable: false),
                        NetRCTarget = c.Double(nullable: false),
                        VolTarget = c.Double(nullable: false),
                        VolMax = c.Double(nullable: false),
                        VolMin = c.Double(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.TreatingSpecID);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        ItemID = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ItemID);
            
            CreateTable(
                "dbo.ReleaseSheet",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemID = c.String(maxLength: 128),
                        ReleaseDateTime = c.DateTime(nullable: false),
                        Job = c.String(),
                        Board = c.String(),
                        Width = c.Int(),
                        Length = c.Int(),
                        Lbs = c.Int(),
                        Sheets = c.Int(),
                        Roll = c.String(),
                        Rejected = c.Int(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Item", t => t.ItemID)
                .Index(t => t.ItemID);
            
            CreateTable(
                "dbo.TreaterTest",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TreatingLineID = c.Int(nullable: false),
                        TreatmentDateTime = c.DateTime(nullable: false),
                        AshPartID = c.String(maxLength: 128),
                        OperatorA = c.Int(nullable: false),
                        OperatorB = c.Int(),
                        RollSkidNum = c.String(),
                        JobNum = c.String(),
                        Qty = c.Int(),
                        UOM = c.Int(),
                        DryRollNum = c.String(),
                        TestNum = c.Int(),
                        Zone1Temp = c.Int(),
                        Zone2Temp = c.Int(),
                        Zone3Temp = c.Int(),
                        Speed = c.Int(),
                        PanConfig = c.Int(),
                        PanLevel = c.Int(),
                        BarConfig = c.Int(),
                        BarAngle = c.Int(),
                        ResinTemp = c.Int(),
                        ResinSolids = c.Int(),
                        RawPaperWeight = c.Int(nullable: false),
                        DryPaperWeight = c.Int(nullable: false),
                        TreatedPaperWeight = c.Int(nullable: false),
                        VolatilesTreatedWeight = c.Int(nullable: false),
                        VolatilesOvenDryWeight = c.Int(nullable: false),
                        TreatedPressedWeight = c.Int(nullable: false),
                        ResinContentCustomer = c.Double(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AshPart", t => t.AshPartID)
                .ForeignKey("dbo.TreatingLine", t => t.TreatingLineID, cascadeDelete: true)
                .Index(t => t.TreatingLineID)
                .Index(t => t.AshPartID);
            
            CreateTable(
                "dbo.TreatingLine",
                c => new
                    {
                        TreatingLineID = c.Int(nullable: false),
                        TreaterName = c.String(),
                    })
                .PrimaryKey(t => t.TreatingLineID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TreaterTest", "TreatingLineID", "dbo.TreatingLine");
            DropForeignKey("dbo.TreaterTest", "AshPartID", "dbo.AshPart");
            DropForeignKey("dbo.ReleaseSheet", "ItemID", "dbo.Item");
            DropForeignKey("dbo.AshPart", "TreatingSpecID", "dbo.TreatingSpec");
            DropIndex("dbo.TreaterTest", new[] { "AshPartID" });
            DropIndex("dbo.TreaterTest", new[] { "TreatingLineID" });
            DropIndex("dbo.ReleaseSheet", new[] { "ItemID" });
            DropIndex("dbo.AshPart", new[] { "TreatingSpecID" });
            DropTable("dbo.TreatingLine");
            DropTable("dbo.TreaterTest");
            DropTable("dbo.ReleaseSheet");
            DropTable("dbo.Item");
            DropTable("dbo.TreatingSpec");
            DropTable("dbo.AshPart");
        }
    }
}
