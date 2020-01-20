using System;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalMedicine.Filters;
using DigitalMedicine.Models;
using DigitalMedicine.Models.MedCard;
using DigitalMedicine.Models.Users;
using DigitalMedicine.Models.KnowledgeBase;

namespace DigitalMedicine.Controllers
{
    [MyAuth]
    [MyAuthorize(Roles = "4")]
    public class DoctorController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        [HttpGet]
        public async Task<ActionResult> ShowAppointments()
        {
            User user = (User)Session["currentUser"];
            List<Appointment> busyTimes = await db.Appointments.Where(r => r.IdDoctor== user.Id).Include(u=>u.Patient).ToListAsync();
            return View(busyTimes);
        }

        [HttpPost]
        public async Task<string> ChangeReceptionTime(int id, int receptionTime)
        {
            Doctor doctor= await db.Doctors.FindAsync(id);
            doctor.ReceptionTime = new TimeSpan(0,receptionTime,0);
            db.Entry(doctor).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return "Время приёма изменено!";
        }

        [HttpPost]
        public async Task<string> ChangeRotationTime(int id, int rotationTime)
        {
            Doctor doctor = await db.Doctors.FindAsync(id);
            TimeSpan rotTime = new TimeSpan(0, rotationTime, 0);
            if (rotationTime!=0 && doctor.ReceptionTime > rotTime)
                return $"Время чередования не должно быть меньше времени приёма ({doctor.ReceptionTime.TotalMinutes} минут)";
            doctor.RotationTime = rotTime;
            db.Entry(doctor).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return "Время чередования изменено!";
        }

        [HttpGet]
        public async Task<ActionResult> ShowPatientInfo(int? id, int? idAppointment)
        {
            User patient=await db.Patients.FindAsync(id),
                doctor = (User)Session["currentUser"];
            //Appointment appointment = await db.Appointments.Where(ap => ap.IdPatient == patient.Id && ap.IdDoctor == doctor.Id).OrderByDescending(ap=>ap.Day).FirstAsync();
            Appointment appointment = db.Appointments.Find(idAppointment);
            List<int> symptomsId=Symptom.GetSymptomsInt(appointment.IdSymptoms);
            List<Symptom> symptoms = new List<Symptom>();
            foreach (var symptomId in symptomsId)
            {
                Symptom symptom = db.Symptoms.Find(symptomId);
                symptoms.Add(symptom);
            }
            ViewBag.appointment = appointment;
            ViewBag.symptoms = symptoms;
            return View(patient);
        }

        public ActionResult GetPatientReports(int idPatient)
        {
            ViewBag.DoctorReports=db.DoctorReports.Where(dr => dr.PatientId == idPatient).OrderByDescending(dr=>dr.Date).ToList<DoctorReport>();
            ViewBag.LabaratoryReports = db.LabaratoryReports.Where(dr => dr.PatientId == idPatient).OrderByDescending(dr => dr.Date).ToList<LaboratoryReport>();
            return View();
        }

        public ActionResult GetSelfReports()
        {
            int idDoctor= ((User)Session["currentUser"]).Id;
            List<DoctorReport> reports = db.DoctorReports.Where(dr => dr.DoctorId == idDoctor).OrderByDescending(dr=>dr.Date).ToList<DoctorReport>();
            return View(reports);
        }

        [HttpPost]
        public async Task<RedirectResult> AddReport(int IdAppointment, [Bind(Exclude = "Date")]DoctorReport report, List<string> symptoms, List<string> complaints ,List<string> complaints2, List<string> analyses)
        {
            report.Complaints = Symptom.GetSymptomsString(complaints);
            report.Complaints2 = Symptom.GetSymptomsString(complaints2);
            report.Analyses = Symptom.GetSymptomsString(analyses);
            report.IdSymptoms = Symptom.GetSymptomsString(symptoms);
            report.Date = DateTime.Now;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Appointment appointment = await db.Appointments.FindAsync(IdAppointment);
                    db.Appointments.Remove(appointment);
                    db.DoctorReports.Add(report);
                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return Redirect("/Cabinet");
        }

        [HttpPost]
        public RedirectResult AddDiagnosis(Diagnosis d)
        {
            db.Diagnoses.Add(d);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}