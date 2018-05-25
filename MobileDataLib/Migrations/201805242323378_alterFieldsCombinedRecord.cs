namespace MobileDataLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterFieldsCombinedRecord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CombinedRecords", "UserName_BlitzData", c => c.String());
            AddColumn("dbo.CombinedRecords", "UserName_DivisionData", c => c.String());
            AddColumn("dbo.CombinedRecords", "UserName_FrancisData", c => c.String());
            AddColumn("dbo.CombinedRecords", "IsNameConsistent", c => c.Boolean(nullable: false));
            DropColumn("dbo.CombinedRecords", "RawUserName_BlitzData");
            DropColumn("dbo.CombinedRecords", "RawUserName_DivisionData");
            DropColumn("dbo.CombinedRecords", "RawUserName_FrancisData");
            DropColumn("dbo.CombinedRecords", "IsNameDiscrepancy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CombinedRecords", "IsNameDiscrepancy", c => c.Boolean(nullable: false));
            AddColumn("dbo.CombinedRecords", "RawUserName_FrancisData", c => c.String());
            AddColumn("dbo.CombinedRecords", "RawUserName_DivisionData", c => c.String());
            AddColumn("dbo.CombinedRecords", "RawUserName_BlitzData", c => c.String());
            DropColumn("dbo.CombinedRecords", "IsNameConsistent");
            DropColumn("dbo.CombinedRecords", "UserName_FrancisData");
            DropColumn("dbo.CombinedRecords", "UserName_DivisionData");
            DropColumn("dbo.CombinedRecords", "UserName_BlitzData");
        }
    }
}
