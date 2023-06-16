namespace EmployeeAppWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEndWorkingDateAsNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "EndWorkingDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "EndWorkingDate", c => c.DateTime(nullable: false));
        }
    }
}
