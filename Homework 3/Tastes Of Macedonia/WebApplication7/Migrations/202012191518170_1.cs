namespace WebApplication7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.mytables",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        lon = c.Decimal(nullable: false, precision: 18, scale: 2),
                        lat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        name = c.String(),
                        cuisine = c.String(),
                        opening_hours = c.String(),
                        website = c.String(),
                        phone = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.mytables");
        }
    }
}
