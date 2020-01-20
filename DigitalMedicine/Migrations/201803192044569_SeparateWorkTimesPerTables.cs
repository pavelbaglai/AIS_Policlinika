namespace DigitalMedicine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeparateWorkTimesPerTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WorkTimes", "Id", "dbo.Doctors");
            DropIndex("dbo.WorkTimes", new[] { "Id" });
            DropPrimaryKey("dbo.WorkTimes");
            CreateTable(
                "dbo.LaboratoryAnalysis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        About = c.String(maxLength: 500),
                        Price = c.Single(nullable: false),
                        IdLaboratory = c.Int(),
                        WorkTime_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Labaratories", t => t.IdLaboratory)
                .ForeignKey("dbo.WorkTimes", t => t.WorkTime_Id)
                .Index(t => t.IdLaboratory)
                .Index(t => t.WorkTime_Id);
            
            AddColumn("dbo.Doctors", "WorkTime_Id", c => c.Int());
            AlterColumn("dbo.WorkTimes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.WorkTimes", "Id");
            CreateIndex("dbo.Doctors", "WorkTime_Id");
            AddForeignKey("dbo.Doctors", "WorkTime_Id", "dbo.WorkTimes", "Id");
            DropColumn("dbo.Institutions", "PriceList");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Institutions", "PriceList", c => c.String(maxLength: 400));
            DropForeignKey("dbo.Doctors", "WorkTime_Id", "dbo.WorkTimes");
            DropForeignKey("dbo.LaboratoryAnalysis", "WorkTime_Id", "dbo.WorkTimes");
            DropForeignKey("dbo.LaboratoryAnalysis", "IdLaboratory", "dbo.Labaratories");
            DropIndex("dbo.Doctors", new[] { "WorkTime_Id" });
            DropIndex("dbo.LaboratoryAnalysis", new[] { "WorkTime_Id" });
            DropIndex("dbo.LaboratoryAnalysis", new[] { "IdLaboratory" });
            DropPrimaryKey("dbo.WorkTimes");
            AlterColumn("dbo.WorkTimes", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Doctors", "WorkTime_Id");
            DropTable("dbo.LaboratoryAnalysis");
            AddPrimaryKey("dbo.WorkTimes", "Id");
            CreateIndex("dbo.WorkTimes", "Id");
            AddForeignKey("dbo.WorkTimes", "Id", "dbo.Doctors", "Id");
        }
    }
}
