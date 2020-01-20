using System;
using System.Data.Entity;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using DigitalMedicine.Models.MedCard;
using DigitalMedicine.Models.News;
using DigitalMedicine.Models.Institution;
using DigitalMedicine.Models.Users;
using DigitalMedicine.Models.KnowledgeBase;

namespace DigitalMedicine.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
            : base("name=DataBaseEntity")
        {
         // Database.SetInitializer<DataBaseContext>(new DataBaseInitializer());
        }

        public virtual DbSet<Institution.Institution> Institutions { get; set; }
        public virtual DbSet<Pharmacy> Pharmacies { get; set; }
        public virtual DbSet<Clinic> Clinics { get; set; }
        public virtual DbSet<Labaratory> Labaratories { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorSpeciality> DoctorSpecialities { get; set; }
        public virtual DbSet<OtherWorker> OtherWorkers { get; set; }
        public virtual DbSet<WorkTime> WorkTimes { get; set; }
        public virtual DbSet<ClinicNews> ClinicNews { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<LaboratoryAppointment> LaboratoryAppointments { get; set; }
        public virtual DbSet<LaboratoryReport> LabaratoryReports { get; set; }
        public virtual DbSet<LaboratoryAnalysis> LaboratoryAnalyses{ get; set; }
        public virtual DbSet<DoctorReport> DoctorReports { get; set; }
        public virtual DbSet<ExceptionDetail> ExceptionDetails { get; set; }
        public virtual DbSet<SymptomGroup> SymptomGroups { get; set; }
        public virtual DbSet<Symptom> Symptoms { get; set; }
        public virtual DbSet<Diagnosis> Diagnoses { get; set; }
        public virtual DbSet<SymptomWeight> SymptomsWeights { get; set; }
    }

    public class DataBaseInitializer : DropCreateDatabaseAlways<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {

            #region Institutions

           // context.Pharmacies.Add(new Pharmacy() { Name = "������ ���������", Phone="+38(050)578-22-99",About = "������ ������ � ��! � ��� ��������-���� ������� ����������� �������� � ������ ����!", Address = "������� 19", Photo = File.ReadAllBytes(@"D:\Diplom\DigitalMedicine\DigitalMedicine\Photos\PharmacyDraw.png") , Latitude= 46.485324, Longitude=30.731064 });

            Clinic c1 = new Clinic() { Name = "��� �� ���", Phone = "+7(473)207-24-00", About = "����������� ��������� ����������� �������� �1 �������� ���������� � ����������-����������� ������� ���������-��������������� �����������, ������������ �������� � ������� ������� ���������� ���������, ��������� ����������� ������� �� �������� ������������������� ����������� ������. ��� ������������ ������ ���������� ���������� �� ���������� ������ ��������. �� ������������� ������� ����������� ������� ������������ ������������������, � ��� ����� ������������������� ����������� ������ � �������������� ����� ��������� ���������� � ����������� �����", Address = "394066, �. �������, ���������� ��������, 151, ������ 1, ��� �� ���� �1", Photo = File.ReadAllBytes(@"D:\�����������\������\DigitalMedicine\Photos\Clinic1.jpg"), Latitude= 51.741559, Longitude = 39.178252 };
            context.Clinics.Add(c1);

            Labaratory l1 = new Labaratory() { Name = "��������", Address =  "�����-���������, �������� �����, 40 ������ 1", Phone = " +7(812)501-15-48", About = "������������� ����������� ������ ������� ������������� ������ � ����� ������������ �������. ����������� � ������� ��������� �� �������� �����, �� �������� ������ ���������� � ��������� ������ ����������.", Photo = File.ReadAllBytes(@"D:\�����������\������\DigitalMedicine\Photos\LaboratoryDraw.png") , Latitude= 59.949395, Longitude = 30.231459 };
            context.Labaratories.Add(l1);

            #endregion

            #region  Roles
            Role r1 = new Role() { Id = 1, Name = "������������� �������", Description = "������� ���� � ���-����������! �������� ���� ��������(���������/������� ��������,������, �����������, ��������� �������������� �������)." };
            Role r2 = new Role() { Id = 2, Name = "������������� �������", Description = "������������ ��� ������� ����������� ������� � ������" };
            Role r3 = new Role() { Id = 3, Name = "��������� �������", Description = "�������� �� �������������� �����(�������, �������� ������������, ���������� �����-����� �����, ��������� ���������� � �������)" };
            Role r4 = new Role() { Id = 4, Name = "������", Description = "�������� ������� �� �����, ���.����� ��������� � ���������� � �� �������" };
            Role r5 = new Role() { Id = 5, Name = "��������", Description = "�������� ���.����� �������� � ���������� �������� �����������" };
            Role r6 = new Role() { Id = 6, Name = "�������", Description = "�������� �������� ����� ���.����� � ������ � �����." };
            Role r7 = new Role() { Id = 7, Name = "������������� �����������", Description = "��������� ��������� � ���������� ����� �������� � ����������." };
            context.Roles.AddRange(new List<Role>() { r1, r2, r3, r4, r5, r6 });

            #endregion

            #region DoctorSpecialities

            List<DoctorSpeciality> specialities = new List<DoctorSpeciality>() {
                new DoctorSpeciality(){ Name="���������", Information="����, ������� ���������� ���������� � �������� ������� ��������� ������������ �����������."},
                new DoctorSpeciality(){ Name="���������", Information="����, ������� ������� ���������� � ����� ����������� ������������� � ������������ ���������������� ������������ ���������� �� ������� � �������� ���������� �����."},
                new DoctorSpeciality(){ Name="���������������", Information="����, ������� ���������������� �� �����������, ������������ � ������� ��������� ����������� ������� ���������-��������� ������." },
                new DoctorSpeciality(){ Name="���������", Information="����, ������� ���������������� �� �����������, ������� � �������������� ������� �����������" },
                new DoctorSpeciality(){ Name="����������", Information="����, ������� ������������ ������� ���� ����� ������ �����������, ������������� ������� ���������. ����� ����� ��� ��������� ������������ �������� ���������� � ����, - � � ���� ������ ��������� ������������ �����������." },
                new DoctorSpeciality(){ Name="��������", Information="����-����������, ������� ���������� ��������� ���������� ������������� �������, � ��� ����� ���������. ��� �������, ������������ ��������� ������������ � ���������-��������� � ���������� �����������." },
                new DoctorSpeciality(){ Name="������ ����", AbilityToPatientRecord=true, Information="���������� � ����� ������� ����������� ����� � ������� ���." },
                new DoctorSpeciality(){ Name="���������", Information="����, ������� ���������������� �� ��������� ������ � �������, � ����� ������������, ����������� � ������� ����� ����������� ��������-���������� �������, ��� ������� ��������, �����������, �������������, ������� � ������." },
                new DoctorSpeciality(){ Name="��������", Information="����-����������, ������� �� ���������������� ������ ������������ �������, ����������� � ������������ ����������� ���������, �������� ����� � �������������� ������� �������." },
                new DoctorSpeciality(){ Name="������������", Information="����, ������� ����� ������� ����������� ������� �������, � ��� ����� ��������� � �������� �����, � ����� �������������� ������." },
                new DoctorSpeciality(){ Name="�������", AbilityToPatientRecord=true, Information="����, � ������������� �������� ������ ����������� � ������� ����������� ���� � ��������������� ������� (������� �����, ���)." },
                new DoctorSpeciality(){ Name="�������", Information="����, ������� ������������ ����������� � ������� ����������������� � ��������������� ���������������: ���� ������ �������, ����������� ����������� � ���� ����." },
                new DoctorSpeciality(){ Name="���������", Information="����, ������� ���������������� �� ������� ����������� ������ ����� � ������� �������. ��� ������ ��������� ��������������� � ������������� ������� ��������� ������������ ������� ��������, �������� ������ ����� � ������ ������� �������." },
                new DoctorSpeciality(){ Name="��������", Information="����������, ������� ���������� ������������, �������� � ������������� ����������� �����������." },
                new DoctorSpeciality(){ Name="����������", Information="����, ������� ���������������� �� ������� ����������� �������������-��������������� ���������, � ���������� ������� ���������� �������������� ������� � �����." },
                new DoctorSpeciality(){ Name="�������� ����", AbilityToPatientRecord=true, Information="����������, ������� ������������ ���������� � ������� ���� ������ ���������� �����. ����� ����������, ��� � ���������������� ����������� �����, �������� ������ ����� ���������� ���������������� ������������������ �� ����������� ��������, ��������� � ���������� ��������." },
                new DoctorSpeciality(){ Name="��������", Information="����, ������� ����� ��������������� ����������� �� �������� �����������, ������������ � ������� �������� ���������� �������." },
                new DoctorSpeciality(){ Name="����������", Information="����-����������, ������� ����������� ���� ������������ �� ������������� �����������, ������� � �������������� ���������� ���������� ��������� (��������� ������������ � �������� ����������)." },
                new DoctorSpeciality(){ Name="�����������", Information="����, ������������ �������� ����� � �������������� ���������. ������ ����� ���������� ����� � ����������� ������������ ������ ������������� ������� ������������, ����������� ������������ � ��������� ���� ����." },
                new DoctorSpeciality(){ Name="������", Information="����������, ������� ������������ �����������, ������� � ������������ ����������� ������� ����������� �������." },
                new DoctorSpeciality(){ Name="������", Information="����, ������� ���������� �������������, ������� ������� ����������� ������� �������." },
                new DoctorSpeciality(){ Name="������������", Information="����������, ������� ����������� ������������ �����������, ������������ � ������� ����������� ����������� �������." }
            };
            context.DoctorSpecialities.AddRange(specialities);

            #endregion

            #region Users

            Doctor d1 = new Doctor()
            {
                Name = "��������",
                Surname = "�����������",
                Patronymic = "���������",
                Role = r4,
                Birthday = new DateTime(1970, 3, 11),
                Information = "���� ������� ���� ������. ������� �����-������������� ����������� ��������������� ����������� ��. ������� (������) � 1990 ����. ����� ����� ������� � ���� �������� ������ �����-����������.",
                Clinic = c1,
                Email = "bere-za@mail.ru",
                Login = "doctor1",
                Password = "doctor1",
                Photo = File.ReadAllBytes(@"D:\�����������\������\DigitalMedicine\Photos\doctor1.png"),
                ReceptionTime = new TimeSpan(0, 30, 0),
                RotationTime = new TimeSpan(1, 20, 0),
                Specialities = new List<DoctorSpeciality>() { specialities[15] },
                WorkTime = new WorkTime() { Monday = "08:00-20:00", Tuesday = "08:00-20:00", Wednesday = "08:00-20:00", Thursday = "08:00-20:00", Friday = "08:00-20:00", Saturday = "12:00-15:00" }
            };
            context.Doctors.Add(d1);
            Doctor d2 = new Doctor()
            {
                Name = "������",
                Surname = "�������",
                Patronymic = "�������",
                Role = r4,
                Birthday = new DateTime(1991, 8, 19),
                Information = "������� ����-����������. ������� ���������� ��������������� ����������� �����������(�������) � 2010 ����. ������� � ������� � 2012 ���� � ���� �����.",
                Clinic = c1,
                Email = "bogdan@ukr.net",
                Login = "doctor2",
                Password = "doctor2",
                Photo = File.ReadAllBytes(@"D:\�����������\������\DigitalMedicine\Photos\doctor2.png"),
                ReceptionTime = new TimeSpan(0, 20, 0),
                RotationTime = new TimeSpan(0, 60, 0),
                Specialities = new List<DoctorSpeciality>() { specialities[2] },
                WorkTime = new WorkTime() { Monday = "10:00-18:30", Tuesday = "10:00-18:30", Wednesday = "10:00-18:30", Thursday = "10:00-18:30", Friday = "10:00-18:30" }
            };
            context.Doctors.Add(d2);

            Patient p1 = new Patient()
            {
                Name = "��",
                Surname = "�������",
                Patronymic = "������������",
                Birthday = new DateTime(1998, 8, 21),
                Role = r6,
                Email = "gorshkov.yan1998@gmail.com",
                ConfirmedEmail = true,
                Login = "patient1",
                Password = "patient1",
                Photo = File.ReadAllBytes(@"D:\�����������\������\DigitalMedicine\Photos\patient1.jpg")
            };
            context.Patients.Add(p1);

            User mainAdmin = new User()
            {
                Name = "�������������",
                Surname = "���������������",
                Patronymic = "�����������������",
                Birthday = new DateTime(1988, 2, 11),
                Role = r1,
                Email = "administrator@gmail.com",
                Login = "administrator",
                Password = "administrator",
            };
            context.Users.Add(mainAdmin);
            #endregion
            #region Reports

            LaboratoryReport lr1 = new LaboratoryReport()
            {
                Date = DateTime.Now,
                Labaratory = l1,
                Description = "������� ����� ����. ������!",
                Files = File.ReadAllBytes(@"D:\�����������\������\DigitalMedicine\LabaratoryArchive\Report1.rar"),
                Patient = p1
            };
            context.LabaratoryReports.Add(lr1);

            #endregion

            #region Symptoms

            List<SymptomGroup> sgroups = new List<SymptomGroup>() {
                    new SymptomGroup(){Name="�����",Information="���� �������� ����� ���������� � �������������, � ������� ������� ����� (��������), �������� (���������), ��������� (�������) � ������������������� ������ (��������). ����� ������� ���� �� �������� ���� �������� � �������� ����������� ������������� ���������� ����������, ��������� ������� �� ������, � ����� ������." },
                    new SymptomGroup(){ Name="������", Information="������ �� ����� ��������� �������������� ������ ����. ���� � ��� ��������� �������� ����, � ����� ������ ������, �����, ��������, �����, ����������, ����, ����������� �������."},
                    new SymptomGroup(){ Name="�����", Information="������� ������, ����� � ���� �� ������ ��������. ���������� ��������, ������, �������������, � ����� �������. ������� ������ �������� � ���� ������� �������, � ����� ��-�� ����������� ��������� ������� ����� ������� �������. ���������� ������ � ������� �� ������� ������ ����������� ����������� ������������ �������� �������."},
                    new SymptomGroup(){ Name="�����", Information="� ������� ������� (��� ��� �������� �������� - � ������) ����������� ������� ������� ������� �����������. ������, ������� � ������������� ������ ��������� � ������� ����� �������, ������� ��� �������� ������������ �� ������� �������."},
                    new SymptomGroup(){ Name="����", Information="���� �������� ����� ������� ������� � ��������� ��������. ������� ���� � ��������������������� ��������� �������� ���������� ����� 1.5-2.3 �^2, ����� ��� ��������� ������� ��������� (���) 4-6%, � ������ � ��� 16-17% �� ������ ���� ����. �� ������ �������� ���� ���� ����������� �� �������, ���������, ������� � ���������� ������� � ������� �����. "},
                    new SymptomGroup(){ Name="����", Information="���� � ��� ������ ����� �������� � ����� ��������. �������� ��� �������� ����� � ���������, ������� ������, ������ (�������, ����, ���������) � ������� ������� (������������ � ��������������)."},
                    new SymptomGroup(){ Name="��� � ������� �������", Information="��� � ������� ������� ������ � �������� ��������� � ������� ����������� ����� ����������� �������."},
                    new SymptomGroup(){ Name="����� ��������", Information="��� ������ ��������������� �� �������� � ��������, ������� ����������� ���� �������� � ����� ��� ��������� ������� ����� ��� �� ��������, ���������� ������� ����� ���� ���������� ��� ������ ������� ��� ������. � �����, � ��� ������ ����� ��� ��������, ������� ���������� ����� ���������������� �� ��������� � ���������, � ������� ������ ������ �� ����� ������������ ��������."},
                    new SymptomGroup(){ Name="������ ������ ����", Information="����� ��� � ��� ������������� ������������, ������� ���������� ������� ����. ������� ������� ���������� ������ �����������, � ����� �������� ����������� �����, ����� ������� ������� � �������."},
                    new SymptomGroup(){ Name="���", Information="���, ��� ������� ������� - ��� ����� ������� ���� ������� �������, ������� ��������� � �����. � ���� ��������� ������� �����, ����� ������� �������� ���������� ������� ����������� ������ ����� � �������� �������."},
                    new SymptomGroup(){ Name="����", Information="���� - ������� ���������� ��������. � ���� �������� ��������� ������������� ������� � ��������������� �� ����� ������ ����:"},
                    new SymptomGroup(){ Name="�����", Information="��� �������� ����� ���� ����� ������� ������� ������, ��� ���������� ������� �� ����. � ������� �������� ��������� �� ������ �� ���������."},
                    new SymptomGroup(){ Name="���", Information="����������� ������� ��� ���� ������� � ���, ��� �������� ������� �������� � ����� ����������� �� ���� ��� ������� ������: � ��� ����������� ���������� ������������ ������� ������ � ��������� (������ � ������� � ����� �� ������ ��������� �������� � ������������� ����� 30 000 �������������� ������). ������� ������ ����������� ����� �������, ��� ��� � ����� ������ ����� ������."},
                    new SymptomGroup(){ Name="���",  Information="�� ������ �� ��, ��� ��� �� ������ - ������ ����� ����� ������������� ���������, ��� �������� � ���� ��������� �������� ������ ��������. �� ������� ������� � ���������� ���� ����� �������, ����������� � ������ (������������ �������), � �������."},
                    new SymptomGroup(){ Name="������� � ����", Information="������� � ��� ������ ������������� �����������, ��������� �� ������ ������ (�����, ��������� ������� ���������, ����) � ������������� �� ������ � �����-������� ����������� ����. ������ ������� ���������� ������ �������� ���������-����������� ����� (���� ���������) � ������ ����������� �����, ����� �������� �������� ������� �� �����, ����� ������������� �������� � ������������."}
                };
            context.SymptomGroups.AddRange(sgroups);

            List<Symptom> eyesSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="���� � �����", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="�������������� ����", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="������� ����� �������", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="������������� � ����", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="����� ��� �������", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="������� ������������ ���� (������� ���)", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="��������� ������", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="������ ������", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="�������� ���", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="�������� ������� � ������", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="����������� �����", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="���������� � ������", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="�������� ������� ������ � ��������, (������� �������)", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="������� ����� ������", SymptomGroup=sgroups[0]},
                    new Symptom(){ Name="���������� (����������)", SymptomGroup=sgroups[0]}
                };
            List<Symptom> headSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="����� ����", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="��������� ����� (��������, ���������, ���������� �����)", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="������������", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="�������� ���� (����� ������)", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="��������������", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="�������� (������������� ���������)", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="�������� ������ (������)", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="�������� ����� (������, �����, ��������� ������)", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="��������� ����������� ������� (������, ����, ����������)", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="��������", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="�������", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="������ ������, �������", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="�������, �������� ����", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="��������� ������", SymptomGroup=sgroups[1]},
                    new Symptom(){ Name="����������� ����", SymptomGroup=sgroups[1]}
                };
            List<Symptom> breastSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="���� � ����� ��� �������", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="����, ����������, ���������� � �������� ������", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="���� � ����� (������� ������)", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="���� � ����� (�������� ������)", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="���� � ������� ������", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="��������� �� ����� � ������", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="������ � ����� (������� ������)", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="������", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="˸������ ������", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="��������� ����� ������", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="������", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="�������� �������� ������� ��� �������", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="������ �������� �������� ��������", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="������", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="��������� �������", SymptomGroup=sgroups[2]},
                    new Symptom(){ Name="��������� ������������", SymptomGroup=sgroups[2]}
                };
            List<Symptom> stomachSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="���� � ������", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="������� ������ (���������)", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="���������, ���������� �������", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="��������� ������� ���������", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="��������� ���� � ������� ������", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="������������ �������", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="�����, ����� � ������", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="�������� ��������", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="�������", SymptomGroup=sgroups[3]},
                    new Symptom(){ Name="������� � �������", SymptomGroup=sgroups[3]}
                };
            List<Symptom> skinSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="������� ���� (���, ��������� �� ����)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="��������", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="��������� � ���� ����� (�������� �����������, ����������� ��� ������� ����)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="��������� � ���� ������ (���������) �� ����", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="������������ ����", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="������������� ����, ������� ����� �� ����", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="�������, ���������� ����", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="������ ����", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="���, ������� ����", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="������ ������� �������� �� ���� (����������)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="������� ������ �� ����, ���������� ���� � ��������� ���������", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="��������� (���������� ����������� ����)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="�������", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="����������� ����", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="���������� �������������", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="������ �� ���� - ��������� � ���� ��������� � ������� (�������)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="����� �� ���� (��������� � ���� �����)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="�����", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="�������� ��������", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="�����, ����, ������������� � ����", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="���������� ��������� (����������������)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="����� ����", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="����", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="������� ���� (���� �� ���� � ����)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="������� (������, ������������, �������)", SymptomGroup=sgroups[4]},
                    new Symptom(){ Name="�����, ���������� �� ���� � ��� �����", SymptomGroup=sgroups[4]}
                };
            List<Symptom> legsSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="���� � ������ (�������� �������)", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="���� � ����� (���� � ������ �����������, ����� ����)", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="���� � ����� (����� �����)", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="���� � ������������� �������", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="���������� �������", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="������������� �����", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="��������� �������", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="���� �� ����� (������� ����, ������� ����)", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="����� ������ �����������", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="�������� ������, �����������, �������������, ���� � ������ �����������", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="�������������� �������", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="����������� �� ���� ������ �����������", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="���������� ������ �������� ������ �����������", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="��������� �������, ������ � ��� �� ���� ������ �����������", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="�������� ����������� ���������� ��� �� �����", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="��������", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="������� � �����", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="��������� ���", SymptomGroup=sgroups[5]},
                    new Symptom(){ Name="�������� �� ����� � ������� ����", SymptomGroup=sgroups[5]}
                };
            List<Symptom> noseAndMouthSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="������������� ���� �� ��������, ���������, ��������, ��� ����������� �� ���", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="���� � �������", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="������������ ����, ����������� � �������", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="����������� �������� �������", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="��� � ����", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="������", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="��������� ����", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="����� � �������: ������ � ������, ����� � ������", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="���������� (�����) ����� �� ����", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="����� �� �����", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="�������", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="���������� ����� ��� ���", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="������� ������������", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="�������", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="���������� ����� ����", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="������� ���������������� ������ ������ ����", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="�����", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="������� �� ���", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="�������, ������� � ������� ���", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="����", SymptomGroup=sgroups[6]},
                    new Symptom(){ Name="�������", SymptomGroup=sgroups[6]}
            };
            List<Symptom> mainSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="��������� (���������� ��������)", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="������, �������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="����", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="���� � ������ (����� �����)", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="���� � ��������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="������� ������������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="������� ����������� ����", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="������ ����, ������ � ���� � �����������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="����������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="���������������������, ����������������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="�������� ��������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="��������� ���", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="���������� �������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="���������������� �������� ������, �������, �������, ���������� ������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="���������� ���������� �����", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="������ ���������� (������)", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="��������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="�����", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="������ �������� � �������� ����", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="��������� ����������� ����", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="���������� ������������ ��������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="���������� ����������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="�������� ��������� �������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="�������� ���������, ���������� �����", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="�������� ����������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="����������������� ����������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="�������� ����������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="����������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="����������� � ������������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="���������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="���������", SymptomGroup=sgroups[7]},
                    new Symptom(){ Name="���������� ���������", SymptomGroup=sgroups[7]},
                };
            List<Symptom> pelvicOrgansSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="���������� ����������� ������������", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="���� - ��������� ��������� �� ���������", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="���� ����� ������ (����� ��� ������)", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="���� �� ����� �������� ���� (�����)", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="���� ��� �����������", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="�������� �����������", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="��� ���������", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="�����������", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="��������� �������������� �����", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="��������� ��������������", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="�� ���������� ������������", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="���������� ����� ����������� ���������", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="������������ ��������", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="�������� �����������", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="���������� ����������� (��� ��������)", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="���������� ��������", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="���������� ������� � ������", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="���������� ����� ����� ��� ������������", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="�������� ������ ���� �������� ���", SymptomGroup=sgroups[8]},
                    new Symptom(){ Name="������� ���������", SymptomGroup=sgroups[8]}
            };
            List<Symptom> groinSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="������ (���������� ����)", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="����� ����� �� ������� �������� �����", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="��������� � �������", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="���� � ���������� � ������� �����������", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="���� � ������� ������� ������� � ������", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="���� � ������", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="��������� �� ������������������� ������ �������� ����� (������) � ������", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="������ � ���� ��� ��������������", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="����� ����", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="����������", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="������� ����� �� ������� ����� � ������", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="����� � ����", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="����� � ������", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="������ ����. ���������� ����", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="���������� ����", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="��������������� ������� ��� ������ ��� ����������� �������� ����", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="������ ���������", SymptomGroup=sgroups[9]},
                    new Symptom(){ Name="������ ��������������", SymptomGroup=sgroups[9]},
                };
            List<Symptom> handsSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="���� � ����� (����� �����)", SymptomGroup=sgroups[10]},
                    new Symptom(){ Name="���� � ����", SymptomGroup=sgroups[10]},
                    new Symptom(){ Name="������ �����", SymptomGroup=sgroups[10]},
                    new Symptom(){ Name="��������� ����� � ����� ������", SymptomGroup=sgroups[10]},
                    new Symptom(){ Name="�������� ������� ���", SymptomGroup=sgroups[10]},
                    new Symptom(){ Name="������ (���� ������)", SymptomGroup=sgroups[10]},
                    new Symptom(){ Name="�������� ����", SymptomGroup=sgroups[10]}
                };
            List<Symptom> backSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="���� � ��������, ���������� �������", SymptomGroup=sgroups[11]},
                    new Symptom(){ Name="���� � �����", SymptomGroup=sgroups[11]},
            };
            List<Symptom> earsSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="���� � ���", SymptomGroup=sgroups[12]},
                    new Symptom(){ Name="������������ ���", SymptomGroup=sgroups[12]},
                    new Symptom(){ Name="������ �����, ��������� �����", SymptomGroup=sgroups[12]},
                    new Symptom(){ Name="�������� ����� (����� ������ ���/���)", SymptomGroup=sgroups[12]},
                    new Symptom(){ Name="�������� (���� � ��� � ����)", SymptomGroup=sgroups[12]}
                };
            List<Symptom> neckSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="���� � �����", SymptomGroup=sgroups[13]},
                    new Symptom(){ Name="���� � ���, ����� �������", SymptomGroup=sgroups[13]},
                    new Symptom(){ Name="������������� (������� � ������), ������ � ������", SymptomGroup=sgroups[13]},
                    new Symptom(){ Name="����������, ������ ������", SymptomGroup=sgroups[13]},
                    new Symptom(){ Name="���������� ����������", SymptomGroup=sgroups[13]},
                    new Symptom(){ Name="����������� �������������", SymptomGroup=sgroups[13]},
                    new Symptom(){ Name="����� �� ����� (���������� ���������� ������)", SymptomGroup=sgroups[13]}
                };
            List<Symptom> buttocksSymptoms = new List<Symptom>() {
                    new Symptom(){ Name="���� � ������������� �������, ���� �������", SymptomGroup=sgroups[14]},
                    new Symptom(){ Name="���� � ������� (����������)", SymptomGroup=sgroups[14]},
                    new Symptom(){ Name="���� � ������� �����", SymptomGroup=sgroups[14]},
                    new Symptom(){ Name="���� � ������� ������", SymptomGroup=sgroups[14]},
                    new Symptom(){ Name="��������� �� ��������� ������", SymptomGroup=sgroups[14]},
                    new Symptom(){ Name="������ (�����)", SymptomGroup=sgroups[14]},
                    new Symptom(){ Name="�����", SymptomGroup=sgroups[14]},
                    new Symptom(){ Name="��� ��� ��������� �� ��������� �������", SymptomGroup=sgroups[14]},
                    new Symptom(){ Name="����� � ���� (�����), ������������ �� ������� ������� (������ �����, �����)", SymptomGroup=sgroups[14]}
                };

            context.Symptoms.AddRange(eyesSymptoms);
            context.Symptoms.AddRange(headSymptoms);
            context.Symptoms.AddRange(breastSymptoms);
            context.Symptoms.AddRange(stomachSymptoms);
            context.Symptoms.AddRange(skinSymptoms);
            context.Symptoms.AddRange(legsSymptoms);
            context.Symptoms.AddRange(noseAndMouthSymptoms);
            context.Symptoms.AddRange(mainSymptoms);
            context.Symptoms.AddRange(pelvicOrgansSymptoms);
            context.Symptoms.AddRange(groinSymptoms);
            context.Symptoms.AddRange(handsSymptoms);
            context.Symptoms.AddRange(backSymptoms);
            context.Symptoms.AddRange(earsSymptoms);
            context.Symptoms.AddRange(neckSymptoms);
            context.Symptoms.AddRange(buttocksSymptoms);

            #endregion

            #region Diagnoses

            List<Diagnosis> diagnoses = new List<Diagnosis>() {
                new Diagnosis(){ Name="��������" },
                new Diagnosis(){ Name="���������" },
                new Diagnosis(){ Name="������� ������" },
                new Diagnosis(){ Name="��������" },
                new Diagnosis(){ Name="������������ �����" },
                new Diagnosis(){ Name="���������" },
                new Diagnosis(){ Name="������� ���������" },
                new Diagnosis(){ Name="��������" },
                new Diagnosis(){ Name="��������" },
                new Diagnosis(){ Name="������� �����" },
                new Diagnosis(){ Name="���������� ������� �����" },
                new Diagnosis(){ Name="��������" },
                new Diagnosis(){ Name="������� �������" },
                new Diagnosis(){ Name="�����������" },
                new Diagnosis(){ Name="��������������� ������� ��������� �����" },
                new Diagnosis(){ Name="������" },
                new Diagnosis(){ Name="������������ �������� ������" },
                new Diagnosis(){ Name="�����������" },
                new Diagnosis(){ Name="������������" },
                new Diagnosis(){ Name="������������" },
                new Diagnosis(){ Name="������������" },
                new Diagnosis(){ Name="�������" },
                new Diagnosis(){ Name="�����" },
                new Diagnosis(){ Name="����������" },
                new Diagnosis(){ Name="���������" },
                new Diagnosis(){ Name="������������ ���������" },
                new Diagnosis(){ Name="��������" },
                new Diagnosis(){ Name="��������" },
                new Diagnosis(){ Name="������ ���������" },
                new Diagnosis(){ Name="�������" },
                new Diagnosis(){ Name="������� ��������" },
                new Diagnosis(){ Name="������������ �����������" },
                new Diagnosis(){ Name="�����������" },
                new Diagnosis(){ Name="�����" },
                new Diagnosis(){ Name="����������� ������� ��������� �����" },
                new Diagnosis(){ Name="��������" },
                new Diagnosis(){ Name="������ �����" },
                new Diagnosis(){ Name="����� �������" },
                new Diagnosis(){ Name="����� �������� ������ ������������" },
                new Diagnosis(){ Name="����" },
                new Diagnosis(){ Name="����������" },
                new Diagnosis(){ Name="����������" },
                new Diagnosis(){ Name="�����������" },
                new Diagnosis(){ Name="������������������" },
                new Diagnosis(){ Name="����� �����" },
                new Diagnosis(){ Name="��������� �������������� ���������" },
                new Diagnosis(){ Name="������������ ��������" },
                new Diagnosis(){ Name="������������ �������� ������ ������������" },
                new Diagnosis(){ Name="������������ ����������� ������ ������������" },
                new Diagnosis(){ Name="������������ ������� ������ ������������" },
                new Diagnosis(){ Name="������ ����������" },
                new Diagnosis(){ Name="������ �������" },
                new Diagnosis(){ Name="������ ����������" },
                new Diagnosis(){ Name="���� ������" },
                new Diagnosis(){ Name="����������" },
                new Diagnosis(){ Name="�������� ���������������" },
                new Diagnosis(){ Name="��� �����" },
                new Diagnosis(){ Name="������������ ��������" },
                new Diagnosis(){ Name="���������� ������ �������������� �������" },
                new Diagnosis(){ Name="�����������" },
                new Diagnosis(){ Name="���������" },
                new Diagnosis(){ Name="�������� ������ 1-�� ����" },
                new Diagnosis(){ Name="�������� ������ 2-�� ����" },
                new Diagnosis(){ Name="��������� ���������������" },
                new Diagnosis(){ Name="�������" },
                new Diagnosis(){ Name="������" },
                new Diagnosis(){ Name="�������������" },
                new Diagnosis(){ Name="�����������" },
                new Diagnosis(){ Name="��������" },
                new Diagnosis(){ Name="�����" },
                new Diagnosis(){ Name="������ ����������" },
                new Diagnosis(){ Name="�������� ����" },
                new Diagnosis(){ Name="������������ ������" },
                new Diagnosis(){ Name="���������" },
                new Diagnosis(){ Name="������" },
                new Diagnosis(){ Name="����������� �������" },
                new Diagnosis(){ Name="������" }
            };

            context.Diagnoses.AddRange(diagnoses);

            #endregion

        }
    }
}