namespace HometownZoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedanimals : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        animalId = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.animalId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Animals");
        }
    }
}
