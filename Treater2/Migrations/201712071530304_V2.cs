namespace Treater2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BarConfig",
                c => new
                    {
                        BarConfigID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.BarConfigID);
            
            AddColumn("dbo.TreaterTest", "BarConfigID", c => c.String(maxLength: 128));
            CreateIndex("dbo.TreaterTest", "BarConfigID");
            AddForeignKey("dbo.TreaterTest", "BarConfigID", "dbo.BarConfig", "BarConfigID");
            DropColumn("dbo.TreaterTest", "BarConfig");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TreaterTest", "BarConfig", c => c.Int());
            DropForeignKey("dbo.TreaterTest", "BarConfigID", "dbo.BarConfig");
            DropIndex("dbo.TreaterTest", new[] { "BarConfigID" });
            DropColumn("dbo.TreaterTest", "BarConfigID");
            DropTable("dbo.BarConfig");
        }
    }
}
