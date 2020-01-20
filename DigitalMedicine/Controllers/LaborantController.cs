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
using DigitalMedicine.Models.MedCard;

namespace DigitalMedicine.Controllers
{
    public class LaborantController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        [HttpGet]
        public async Task<ActionResult> ShowAppointments()
        {
            OtherWorker u = (OtherWorker)Session["currentUser"];
            List<LaboratoryAppointment> apps = await db.LaboratoryAppointments.Where(la => la.LaboratoryAnalysis.IdLaboratory == u.IdInstitution).ToListAsync();
            return View(apps);
        }

        [HttpGet]
        public async Task<ActionResult> ShowAppointment(int id)
        {
            LaboratoryAppointment la = await db.LaboratoryAppointments.FindAsync(id);
            return View(la);
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> AddReport([Bind(Exclude ="Files")]LaboratoryReport report, HttpPostedFileBase files)
        {
            report.Date = DateTime.Now;
            if (files != null)
            {
                byte[] filesData = new byte[files.ContentLength];
                await files.InputStream.ReadAsync(filesData, 0, files.ContentLength);
                report.Files = filesData;
            }
            db.LabaratoryReports.Add(report);
            await db.SaveChangesAsync();
            return RedirectToAction("Index","Cabinet");
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
