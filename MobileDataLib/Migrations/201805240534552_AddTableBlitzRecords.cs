namespace MobileDataLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableBlitzRecords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlitzRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MobileNumber = c.String(),
                        RawUserName = c.String(),
                        SanitisedUserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        IMSI = c.String(),
                        SIM_Number = c.String(),
                        SIM_SerialNumber = c.String(),
                        IMEI = c.String(),
                        Make = c.String(),
                        DeviceName = c.String(),
                        OS = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BlitzRecords");
        }
    }
}
