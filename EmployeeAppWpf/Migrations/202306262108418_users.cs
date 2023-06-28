namespace EmployeeAppWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class users : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Login", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            DropColumn("dbo.Users", "Name");
            DropColumn("dbo.Users", "AccesLevelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "AccesLevelId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String());
            DropColumn("dbo.Users", "Login");
        }
    }
}
