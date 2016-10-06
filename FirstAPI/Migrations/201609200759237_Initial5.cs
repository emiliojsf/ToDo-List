namespace FirstAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Hours", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "Minute", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Minute");
            DropColumn("dbo.Items", "Hours");
        }
    }
}
