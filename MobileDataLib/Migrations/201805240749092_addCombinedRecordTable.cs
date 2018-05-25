namespace MobileDataLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCombinedRecordTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CombinedRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MobileNumber = c.String(),
                        IsDataSourceBlitzData = c.Boolean(nullable: false),
                        IsDataSourceDivisionDevice = c.Boolean(nullable: false),
                        IsDataSourceFrancis = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CombinedRecords");
        }
    }
}
