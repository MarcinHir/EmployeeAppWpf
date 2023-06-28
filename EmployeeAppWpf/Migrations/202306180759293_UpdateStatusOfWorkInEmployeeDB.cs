namespace EmployeeAppWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStatusOfWorkInEmployeeDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "FireOrNot", c => c.Boolean(nullable: false));
            DropColumn("dbo.Employees", "StatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "StatusId", c => c.Int(nullable: false));
            DropColumn("dbo.Employees", "FireOrNot");
        }
    }
}
