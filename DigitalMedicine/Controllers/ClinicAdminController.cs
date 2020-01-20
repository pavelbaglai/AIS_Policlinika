using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DigitalMedicine.Models;
using DigitalMedicine.Models.Users;
using DigitalMedicine.App_Code;
using DigitalMedicine.Filters;

namespace DigitalMedicine.Controllers
{
    [MyAuthorize(Roles = "2")]
    public class ClinicAdminController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        [HttpGet]
        public ActionResult AddDoctor()
        {
            User ow = (User)Session["currentUser"];
            int idRole = db.Roles.Where(r => r.Name == "Доктор").Single().Id;
            ViewBag.IdRole = idRole;
            ViewBag.IdClinic = db.OtherWorkers.Find(ow.Id).IdInstitution;
            return View();
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> AddDoctor([Bind(Exclude = "Password,ReceptionTime,RotationTime")]Doctor d, HttpPostedFileBase photo, int receptionTime, int rotationTime, List<string> SpecialityList)
        {
            d.ReceptionTime = new TimeSpan(0, receptionTime, 0);
            d.RotationTime = new TimeSpan(0, rotationTime, 0);
            string tempPassword = "temppassword";
            d.Password = tempPassword;
            if (photo != null)
            {
                byte[] imageData = new byte[photo.ContentLength];
                await photo.InputStream.ReadAsync(imageData, 0, photo.ContentLength);
                d.Photo = imageData;
                d.cutPhoto();
            }
            d.Specialities = new List<DoctorSpeciality>(SpecialityList.Count);
            foreach (var speciality in SpecialityList)
            {
                int id = int.Parse(speciality);
                d.Specialities.Add(db.DoctorSpecialities.Find(id));
            }
            db.Clinics.Where(c => c.Id == d.IdClinic).Single().Doctors.Add(d);
            await db.SaveChangesAsync();
            MailSender send = new MailSender(d.Email);
            send.SendMessageForConfirmAccount(Url.Action("ConfirmEmail", "Cabinet", new { id = d.Id, email = d.Email }, Request.Url.Scheme), tempPassword);
            return RedirectToAction("Index", "Cabinet");
        }

        [HttpGet]
        public ActionResult AddModerator()
        {
            User ow = (User)Session["currentUser"];
            int idRole = db.Roles.Where(r => r.Name == "Модератор клиники").Single().Id;
            ViewBag.IdRole = idRole;
            ViewBag.IdClinic = db.OtherWorkers.Find(ow.Id).IdInstitution;
            return View();
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> AddModerator([Bind(Exclude = "Password")]OtherWorker ow, HttpPostedFileBase photo)
        {
            ow.Password = "temppassword";
            if (photo != null)
            {
                byte[] imageData = new byte[photo.ContentLength];
                await photo.InputStream.ReadAsync(imageData, 0, photo.ContentLength);
                ow.Photo = imageData;
                ow.cutPhoto();
            }
            db.OtherWorkers.Add(ow);
            await db.SaveChangesAsync();
            MailSender send = new MailSender(ow.Email);
            send.SendMessageForConfirmAccount(Url.Action("ConfirmEmail", "Cabinet", new { id = ow.Id, email = ow.Email }, Request.Url.Scheme), "temppassword");
            return RedirectToAction("Index", "Cabinet");
        }

        [HttpGet]
        public async Task<ActionResult> RemoveModerator()
        {
            int clinicId = ((OtherWorker)Session["currentUser"]).IdInstitution;
            List<OtherWorker> moderators = await db.OtherWorkers.Where(ow => ow.IdInstitution == clinicId && ow.Role.Name=="Модератор клиники").ToListAsync();
            return View(moderators);
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> RemoveModerator(int idModerator)
        {
            OtherWorker moderator= await db.OtherWorkers.FindAsync(idModerator);
            db.OtherWorkers.Remove(moderator);
            await db.SaveChangesAsync();
            return RedirectToAction("Index","Cabinet");
        }

        [HttpGet]
        public async Task<ActionResult> RemoveDoctor()
        {
            int clinicId = ((OtherWorker)Session["currentUser"]).IdInstitution;
            List<Doctor> doctors = await db.Doctors.Where(ow => ow.IdClinic == clinicId).ToListAsync();
            return View(doctors);
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> RemoveDoctor(int idDoctor)
        {
            Doctor doctor = await db.Doctors.FindAsync(idDoctor);
            db.Appointments.RemoveRange(db.Appointments.Where(app => app.IdDoctor == doctor.Id));
            doctor.IdClinic = null;
            db.WorkTimes.Remove(doctor.WorkTime);
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
