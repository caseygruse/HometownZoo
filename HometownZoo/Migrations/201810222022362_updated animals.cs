namespace HometownZoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedanimals : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animals", "sex", c => c.String());
            AddColumn("dbo.Animals", "species", c => c.String());
            AlterColumn("dbo.Animals", "name", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Animals", "name", c => c.String());
            DropColumn("dbo.Animals", "species");
            DropColumn("dbo.Animals", "sex");
        }
    }
}
