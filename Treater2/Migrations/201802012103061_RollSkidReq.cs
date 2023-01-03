namespace Treater2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RollSkidReq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TreaterTest", "RollSkidNum", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TreaterTest", "RollSkidNum", c => c.String());
        }
    }
}
