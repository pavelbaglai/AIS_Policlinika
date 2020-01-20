namespace DigitalMedicine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdSymptoms = c.String(nullable: false, maxLength: 400),
                        PredictedDiagnoses = c.String(maxLength: 200),
                        Day = c.DateTime(nullable: false),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        IdDoctor = c.Int(),
                        IdPatient = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.IdDoctor)
                .ForeignKey("dbo.Patients", t => t.IdPatient)
                .Index(t => t.IdDoctor)
                .Index(t => t.IdPatient);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                        Surname = c.String(nullable: false, maxLength: 30),
                        Patronymic = c.String(nullable: false, maxLength: 25),
                        Birthday = c.DateTime(nullable: false),
                        Email = c.String(nullable: false, maxLength: 30),
                        Login = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 30),
                        Photo = c.Binary(),
                        IdRole = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.IdRole)
                .Index(t => t.IdRole);
            
            CreateTable(
                "dbo.Institutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        About = c.String(maxLength: 1000),
                        Address = c.String(nullable: false, maxLength: 30),
                        PriceList = c.String(maxLength: 400),
                        Phone = c.String(maxLength: 17),
                        Photo = c.Binary(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 300),
                        MiniNews = c.String(nullable: false, maxLength: 500),
                        FullNews = c.String(nullable: false, maxLength: 1500),
                        Date = c.DateTime(nullable: false),
                        Photo = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 200),
                        PublicationTime = c.DateTime(nullable: false),
                        IdUser = c.Int(),
                        IdNews = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.IdNews)
                .ForeignKey("dbo.Patients", t => t.IdUser)
                .Index(t => t.IdUser)
                .Index(t => t.IdNews);
            
            CreateTable(
                "dbo.DoctorReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Appointment = c.String(nullable: false, maxLength: 400),
                        Description = c.String(maxLength: 200),
                        Recommendation = c.String(maxLength: 300),
                        IdSymptoms = c.String(nullable: false, maxLength: 400),
                        DiagnosisId = c.Int(),
                        DoctorId = c.Int(),
                        PatientId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Diagnosis", t => t.DiagnosisId)
                .ForeignKey("dbo.Doctors", t => t.DoctorId)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .Index(t => t.DiagnosisId)
                .Index(t => t.DoctorId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Diagnosis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LaboratoryReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(nullable: false, maxLength: 300),
                        Files = c.Binary(),
                        LabaratoryrId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Labaratories", t => t.LabaratoryrId)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .Index(t => t.LabaratoryrId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DoctorSpecialities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Information = c.String(nullable: false, maxLength: 500),
                        AbilityToPatientRecord = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Monday = c.String(nullable: false, maxLength: 11),
                        Tuesday = c.String(nullable: false, maxLength: 11),
                        Wednesday = c.String(nullable: false, maxLength: 11),
                        Thursday = c.String(nullable: false, maxLength: 11),
                        Friday = c.String(nullable: false, maxLength: 11),
                        Saturday = c.String(nullable: false, maxLength: 11),
                        Sunday = c.String(nullable: false, maxLength: 11),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ExceptionDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExceptionMessage = c.String(),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        StackTrace = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SymptomGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                        Information = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Symptoms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 80),
                        IdSymptomGroup = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SymptomGroups", t => t.IdSymptomGroup)
                .Index(t => t.IdSymptomGroup);
            
            CreateTable(
                "dbo.SymptomWeights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdSymptom = c.Int(),
                        IdDiagnosis = c.Int(),
                        Weight = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Diagnosis", t => t.IdDiagnosis)
                .ForeignKey("dbo.Symptoms", t => t.IdSymptom)
                .Index(t => t.IdSymptom)
                .Index(t => t.IdDiagnosis);
            
            CreateTable(
                "dbo.DoctorSpecialityDoctors",
                c => new
                    {
                        DoctorSpeciality_Id = c.Int(nullable: false),
                        Doctor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DoctorSpeciality_Id, t.Doctor_Id })
                .ForeignKey("dbo.DoctorSpecialities", t => t.DoctorSpeciality_Id, cascadeDelete: true)
                .ForeignKey("dbo.Doctors", t => t.Doctor_Id, cascadeDelete: true)
                .Index(t => t.DoctorSpeciality_Id)
                .Index(t => t.Doctor_Id);
            
            CreateTable(
                "dbo.ClinicNews",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        IdClinic = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.Id)
                .ForeignKey("dbo.Clinics", t => t.IdClinic)
                .Index(t => t.Id)
                .Index(t => t.IdClinic);
            
            CreateTable(
                "dbo.Clinics",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Institutions", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Information = c.String(nullable: false, maxLength: 800),
                        ReceptionTime = c.Time(nullable: false, precision: 7),
                        RotationTime = c.Time(nullable: false, precision: 7),
                        IdClinic = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .ForeignKey("dbo.Clinics", t => t.IdClinic)
                .Index(t => t.Id)
                .Index(t => t.IdClinic);
            
            CreateTable(
                "dbo.Labaratories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Institutions", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.OtherWorkers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Clinic_Id = c.Int(),
                        IdInstitution = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .ForeignKey("dbo.Clinics", t => t.Clinic_Id)
                .ForeignKey("dbo.Institutions", t => t.IdInstitution, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.Clinic_Id)
                .Index(t => t.IdInstitution);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ConfirmedEmail = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Pharmacies",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Institutions", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pharmacies", "Id", "dbo.Institutions");
            DropForeignKey("dbo.Patients", "Id", "dbo.Users");
            DropForeignKey("dbo.OtherWorkers", "IdInstitution", "dbo.Institutions");
            DropForeignKey("dbo.OtherWorkers", "Clinic_Id", "dbo.Clinics");
            DropForeignKey("dbo.OtherWorkers", "Id", "dbo.Users");
            DropForeignKey("dbo.Labaratories", "Id", "dbo.Institutions");
            DropForeignKey("dbo.Doctors", "IdClinic", "dbo.Clinics");
            DropForeignKey("dbo.Doctors", "Id", "dbo.Users");
            DropForeignKey("dbo.Clinics", "Id", "dbo.Institutions");
            DropForeignKey("dbo.ClinicNews", "IdClinic", "dbo.Clinics");
            DropForeignKey("dbo.ClinicNews", "Id", "dbo.News");
            DropForeignKey("dbo.Users", "IdRole", "dbo.Roles");
            DropForeignKey("dbo.SymptomWeights", "IdSymptom", "dbo.Symptoms");
            DropForeignKey("dbo.SymptomWeights", "IdDiagnosis", "dbo.Diagnosis");
            DropForeignKey("dbo.Symptoms", "IdSymptomGroup", "dbo.SymptomGroups");
            DropForeignKey("dbo.WorkTimes", "Id", "dbo.Doctors");
            DropForeignKey("dbo.DoctorSpecialityDoctors", "Doctor_Id", "dbo.Doctors");
            DropForeignKey("dbo.DoctorSpecialityDoctors", "DoctorSpeciality_Id", "dbo.DoctorSpecialities");
            DropForeignKey("dbo.LaboratoryReports", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.LaboratoryReports", "LabaratoryrId", "dbo.Labaratories");
            DropForeignKey("dbo.DoctorReports", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.DoctorReports", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.DoctorReports", "DiagnosisId", "dbo.Diagnosis");
            DropForeignKey("dbo.Comments", "IdUser", "dbo.Patients");
            DropForeignKey("dbo.Appointments", "IdPatient", "dbo.Patients");
            DropForeignKey("dbo.Comments", "IdNews", "dbo.News");
            DropForeignKey("dbo.Appointments", "IdDoctor", "dbo.Doctors");
            DropIndex("dbo.Pharmacies", new[] { "Id" });
            DropIndex("dbo.Patients", new[] { "Id" });
            DropIndex("dbo.OtherWorkers", new[] { "IdInstitution" });
            DropIndex("dbo.OtherWorkers", new[] { "Clinic_Id" });
            DropIndex("dbo.OtherWorkers", new[] { "Id" });
            DropIndex("dbo.Labaratories", new[] { "Id" });
            DropIndex("dbo.Doctors", new[] { "IdClinic" });
            DropIndex("dbo.Doctors", new[] { "Id" });
            DropIndex("dbo.Clinics", new[] { "Id" });
            DropIndex("dbo.ClinicNews", new[] { "IdClinic" });
            DropIndex("dbo.ClinicNews", new[] { "Id" });
            DropIndex("dbo.DoctorSpecialityDoctors", new[] { "Doctor_Id" });
            DropIndex("dbo.DoctorSpecialityDoctors", new[] { "DoctorSpeciality_Id" });
            DropIndex("dbo.SymptomWeights", new[] { "IdDiagnosis" });
            DropIndex("dbo.SymptomWeights", new[] { "IdSymptom" });
            DropIndex("dbo.Symptoms", new[] { "IdSymptomGroup" });
            DropIndex("dbo.WorkTimes", new[] { "Id" });
            DropIndex("dbo.LaboratoryReports", new[] { "PatientId" });
            DropIndex("dbo.LaboratoryReports", new[] { "LabaratoryrId" });
            DropIndex("dbo.DoctorReports", new[] { "PatientId" });
            DropIndex("dbo.DoctorReports", new[] { "DoctorId" });
            DropIndex("dbo.DoctorReports", new[] { "DiagnosisId" });
            DropIndex("dbo.Comments", new[] { "IdNews" });
            DropIndex("dbo.Comments", new[] { "IdUser" });
            DropIndex("dbo.Users", new[] { "IdRole" });
            DropIndex("dbo.Appointments", new[] { "IdPatient" });
            DropIndex("dbo.Appointments", new[] { "IdDoctor" });
            DropTable("dbo.Pharmacies");
            DropTable("dbo.Patients");
            DropTable("dbo.OtherWorkers");
            DropTable("dbo.Labaratories");
            DropTable("dbo.Doctors");
            DropTable("dbo.Clinics");
            DropTable("dbo.ClinicNews");
            DropTable("dbo.DoctorSpecialityDoctors");
            DropTable("dbo.SymptomWeights");
            DropTable("dbo.Symptoms");
            DropTable("dbo.SymptomGroups");
            DropTable("dbo.ExceptionDetails");
            DropTable("dbo.WorkTimes");
            DropTable("dbo.DoctorSpecialities");
            DropTable("dbo.Roles");
            DropTable("dbo.LaboratoryReports");
            DropTable("dbo.Diagnosis");
            DropTable("dbo.DoctorReports");
            DropTable("dbo.Comments");
            DropTable("dbo.News");
            DropTable("dbo.Institutions");
            DropTable("dbo.Users");
            DropTable("dbo.Appointments");
        }
    }
}
