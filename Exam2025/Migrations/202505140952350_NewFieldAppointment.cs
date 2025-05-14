namespace Exam2025.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFieldAppointment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "AppointmentTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "AppointmentTime");
        }
    }
}
