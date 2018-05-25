namespace MobileDataLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableFrancisData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FrancisDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MobileNumber = c.String(),
                        DeviceType = c.String(),
                        RawUserName = c.String(),
                        SanitizedUserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FrancisDatas");
        }
    }
}
