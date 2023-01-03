namespace Treater2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class COA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TreaterTest", "Paper_COA", c => c.String());
            AddColumn("dbo.TreaterTest", "Resin_COA", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TreaterTest", "Resin_COA");
            DropColumn("dbo.TreaterTest", "Paper_COA");
        }
    }
}
