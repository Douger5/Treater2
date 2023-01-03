namespace Treater2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTreatingSpecFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TreatingSpec", "NetRCMin", c => c.Double(nullable: false));
            AddColumn("dbo.TreatingSpec", "NetRCMax", c => c.Double(nullable: false));
            AddColumn("dbo.TreatingSpec", "GurleyPorosityTarget", c => c.Double(nullable: false));
            AddColumn("dbo.TreatingSpec", "GurleyPorosityMin", c => c.Double(nullable: false));
            AddColumn("dbo.TreatingSpec", "GurleyPorosityMax", c => c.Double(nullable: false));
            AddColumn("dbo.TreatingSpec", "WetOutTarget", c => c.Double(nullable: false));
            AddColumn("dbo.TreatingSpec", "WetOutMin", c => c.Double(nullable: false));
            AddColumn("dbo.TreatingSpec", "WetOutMax", c => c.Double(nullable: false));
            AddColumn("dbo.TreatingSpec", "CaliperTarget", c => c.Double(nullable: false));
            AddColumn("dbo.TreatingSpec", "CaliperMin", c => c.Double(nullable: false));
            AddColumn("dbo.TreatingSpec", "CaliperMax", c => c.Double(nullable: false));
            AddColumn("dbo.TreatingSpec", "BasisWeightTarget", c => c.Double(nullable: false));
            AddColumn("dbo.TreatingSpec", "BasisWeightMin", c => c.Double(nullable: false));
            AddColumn("dbo.TreatingSpec", "BasisWeightMax", c => c.Double(nullable: false));
            AddColumn("dbo.TreaterTest", "GurleyPorosity", c => c.Double(nullable: false));
            AddColumn("dbo.TreaterTest", "WetOut", c => c.Double(nullable: false));
            AddColumn("dbo.TreaterTest", "Caliper", c => c.Double(nullable: false));
            DropColumn("dbo.TreaterTest", "ResinContentCustomer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TreaterTest", "ResinContentCustomer", c => c.Double());
            DropColumn("dbo.TreaterTest", "Caliper");
            DropColumn("dbo.TreaterTest", "WetOut");
            DropColumn("dbo.TreaterTest", "GurleyPorosity");
            DropColumn("dbo.TreatingSpec", "BasisWeightMax");
            DropColumn("dbo.TreatingSpec", "BasisWeightMin");
            DropColumn("dbo.TreatingSpec", "BasisWeightTarget");
            DropColumn("dbo.TreatingSpec", "CaliperMax");
            DropColumn("dbo.TreatingSpec", "CaliperMin");
            DropColumn("dbo.TreatingSpec", "CaliperTarget");
            DropColumn("dbo.TreatingSpec", "WetOutMax");
            DropColumn("dbo.TreatingSpec", "WetOutMin");
            DropColumn("dbo.TreatingSpec", "WetOutTarget");
            DropColumn("dbo.TreatingSpec", "GurleyPorosityMax");
            DropColumn("dbo.TreatingSpec", "GurleyPorosityMin");
            DropColumn("dbo.TreatingSpec", "GurleyPorosityTarget");
            DropColumn("dbo.TreatingSpec", "NetRCMax");
            DropColumn("dbo.TreatingSpec", "NetRCMin");
        }
    }
}
