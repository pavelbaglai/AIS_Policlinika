using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
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
    [MyAuthorize(Roles = "6")]
    public class PatientController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        [HttpGet]
        public async Task<ActionResult> DoctorReports()
        {
            User user = (User)Session["currentUser"];
            List<DoctorReport> reports =await db.DoctorReports.Where(r => r.PatientId == user.Id).ToListAsync();
            return View(reports);
        }

        [HttpGet]
        public async Task<ActionResult> DoctorReport(int id)
        {
            User user = (User)Session["currentUser"];
            DoctorReport report = await db.DoctorReports.FindAsync(id);
            List<int> symptomsId = Symptom.GetSymptomsInt(report.IdSymptoms);
            List<string> symptoms = new List<string>();
            foreach (var symptomId in symptomsId)
            {
                Symptom s = db.Symptoms.Find(symptomId);
                symptoms.Add(s.Name);
            }
            ViewBag.symptoms = symptoms;
            return View(report);
        }

        [HttpGet]
        public ActionResult LabaratoryReports()
        {
            User user = (User)Session["currentUser"];
            List<LaboratoryReport> reports = db.LabaratoryReports.Where(lr => lr.PatientId == user.Id).ToList();
            return View(reports);
        }

        [HttpGet]
        [ExceptionLogger]
        public async Task<ActionResult> Appointments()
        {
            int id = ((User)Session["currentUser"]).Id;
            List<Appointment> apps = await db.Appointments.Where(da => da.IdPatient == id).ToListAsync<Appointment>();
            return View(apps);
        }

        [HttpGet]
        [ExceptionLogger]
        public async Task<ActionResult> AnalysesAppointments()
        {
            int id = ((User)Session["currentUser"]).Id;
            List<LaboratoryAppointment> apps = await db.LaboratoryAppointments.Where(la => la.IdPatient == id).ToListAsync<LaboratoryAppointment>();
            return View(apps);
        }

        public async Task<RedirectToRouteResult> SignContract(int idDoctor)
        {
            int idPatient = ((User)Session["currentUser"]).Id;
            Patient p = await db.Patients.FindAsync(idPatient);
            p.IdFamilyDoctor = idDoctor;
            await db.SaveChangesAsync();
            return RedirectToAction("Doctor","Home",new {id=idDoctor });
        }

        public async Task<RedirectToRouteResult> CancelContract()
        {
            int idPatient = ((User)Session["currentUser"]).Id;
            Patient p = await db.Patients.FindAsync(idPatient);
            p.IdFamilyDoctor=null;
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Cabinet");
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
