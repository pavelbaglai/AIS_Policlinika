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
using DigitalMedicine.Models.KnowledgeBase;
using DigitalMedicine.Models.Users;
using DigitalMedicine.Models.Institution;

namespace DigitalMedicine.Controllers
{
    public class LaboratoryAdminController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        [HttpGet]
        public ActionResult AddAnalysis()
        {
            return View();
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> AddAnalysis([Bind(Exclude = "ReceptionTime")]LaboratoryAnalysis analysis, int receptionTime)
        { 
            analysis.ReceptionTime= new TimeSpan(0, receptionTime, 0);
            db.LaboratoryAnalyses.Add(analysis);
            await db.SaveChangesAsync();
            return RedirectToAction("Index","Cabinet");
        }

        [HttpGet]
        public ActionResult AddLaborant()
        {
            ViewBag.IdRole = db.Roles.Where(r=>r.Name=="Лаборант").Single().Id;
            ViewBag.IdLaboratory = ((OtherWorker)Session["currentUser"]).IdInstitution;
            ViewBag.Password = "temppassword";
            return View();
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> AddLaborant(OtherWorker ow, HttpPostedFileBase photo)
        {
            ow.SetPhoto(photo);
            db.OtherWorkers.Add(ow);
            await db.SaveChangesAsync();
            return RedirectToAction("Index","Cabinet");
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> EditLaboratoryInfo(int Id, string Name, string About, string Phone)
        {
            Labaratory laboratory = await db.Labaratories.FindAsync(Id);
            laboratory.Name = Name;
            laboratory.About = About;
            laboratory.Phone = Phone;
            await db.SaveChangesAsync();
            return RedirectToAction("Laboratory", "Home", new { id = Id });
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
