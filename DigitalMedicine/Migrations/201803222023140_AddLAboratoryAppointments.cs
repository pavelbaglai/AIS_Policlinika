namespace DigitalMedicine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLAboratoryAppointments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LaboratoryAppointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.DateTime(nullable: false),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        IdLabaratory = c.Int(),
                        IdPatient = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Labaratories", t => t.IdLabaratory)
                .ForeignKey("dbo.Patients", t => t.IdPatient)
                .Index(t => t.IdLabaratory)
                .Index(t => t.IdPatient);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LaboratoryAppointments", "IdPatient", "dbo.Patients");
            DropForeignKey("dbo.LaboratoryAppointments", "IdLabaratory", "dbo.Labaratories");
            DropIndex("dbo.LaboratoryAppointments", new[] { "IdPatient" });
            DropIndex("dbo.LaboratoryAppointments", new[] { "IdLabaratory" });
            DropTable("dbo.LaboratoryAppointments");
        }
    }
}
