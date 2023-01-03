namespace Treater2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResinSolidsToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TreaterTest", "ResinSolids", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TreaterTest", "ResinSolids", c => c.Int());
        }
    }
}
