namespace EmployeeAppWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserLogin", c => c.String(nullable: false));
            AddColumn("dbo.Users", "UserPassword", c => c.String(nullable: false));
            DropColumn("dbo.Users", "Login");
            DropColumn("dbo.Users", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AddColumn("dbo.Users", "Login", c => c.String(nullable: false));
            DropColumn("dbo.Users", "UserPassword");
            DropColumn("dbo.Users", "UserLogin");
        }
    }
}
