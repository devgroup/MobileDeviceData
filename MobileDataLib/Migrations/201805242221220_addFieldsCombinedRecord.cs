namespace MobileDataLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldsCombinedRecord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CombinedRecords", "RawUserName_BlitzData", c => c.String());
            AddColumn("dbo.CombinedRecords", "RawUserName_DivisionData", c => c.String());
            AddColumn("dbo.CombinedRecords", "RawUserName_FrancisData", c => c.String());
            AddColumn("dbo.CombinedRecords", "IsNameDiscrepancy", c => c.Boolean(nullable: false));
            AddColumn("dbo.DivisionDevices", "RawUserName", c => c.String());
            DropColumn("dbo.DivisionDevices", "RawName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DivisionDevices", "RawName", c => c.String());
            DropColumn("dbo.DivisionDevices", "RawUserName");
            DropColumn("dbo.CombinedRecords", "IsNameDiscrepancy");
            DropColumn("dbo.CombinedRecords", "RawUserName_FrancisData");
            DropColumn("dbo.CombinedRecords", "RawUserName_DivisionData");
            DropColumn("dbo.CombinedRecords", "RawUserName_BlitzData");
        }
    }
}
