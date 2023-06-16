namespace EmployeeAppWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeChangingColumnTypeInDatabase2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "StartWorkingDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "StartWorkingDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
