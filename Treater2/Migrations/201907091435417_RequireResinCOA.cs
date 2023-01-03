namespace Treater2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequireResinCOA : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TreaterTest", "Resin_COA", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TreaterTest", "Resin_COA", c => c.String());
        }
    }
}
