using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;
using System.Web.Mvc;
using DigitalMedicine.Models;
using DigitalMedicine.Models.Users;
using DigitalMedicine.Models.News;
using DigitalMedicine.Models.Institution;

namespace DigitalMedicine.Controllers
{
    public class ModeratorController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        [HttpGet]
        public ActionResult AddNews()
        {
            User moderator = (User)Session["currentUser"];
            ViewBag.IdClinic = db.OtherWorkers.Find(moderator.Id).IdInstitution;
            return View();
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> AddNews([Bind(Exclude ="Date")]ClinicNews news, HttpPostedFileBase photo)
        {
            news.Date = DateTime.Now;
            if (photo != null)
            {
                byte[] imageData = new byte[photo.ContentLength];
                await photo.InputStream.ReadAsync(imageData, 0, photo.ContentLength);
                news.Photo = imageData;
            }
            db.ClinicNews.Add(news);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Cabinet");
        }

        public async Task<RedirectToRouteResult> DeleteNews(int idClinic, int idNews)
        {
            ClinicNews news = db.ClinicNews.Find(idNews);
            db.Comments.RemoveRange(news.Comments);
            db.ClinicNews.Remove(news);
            await db.SaveChangesAsync();
            return RedirectToAction("Clinic", "Home", new { id = idClinic});
        }

        public async Task<RedirectToRouteResult> DeleteComment(int idNews, int idComment)
        {
            db.Comments.Remove(db.Comments.Find(idComment));
            await db.SaveChangesAsync();
            return RedirectToAction("News","Home", new { id=idNews});
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> EditNews(ClinicNews news)
        {
            ClinicNews oldNews =await db.ClinicNews.FindAsync(news.Id);
            oldNews.Title = news.Title;
            oldNews.MiniNews = news.MiniNews;
            oldNews.FullNews = news.FullNews;
            await db.SaveChangesAsync();
            return RedirectToAction("News","Home",new { id=news.Id});
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> EditClinicInfo(int Id, string Name, string About, string Phone)
        {
            Clinic clinic = await db.Clinics.FindAsync(Id);
            clinic.Name = Name;
            clinic.About = About;
            clinic.Phone = Phone;
            await db.SaveChangesAsync();
            return RedirectToAction("Clinic", "Home", new { id = Id });
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
