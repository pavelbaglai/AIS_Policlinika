using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Threading.Tasks;
using DigitalMedicine.Models;
using DigitalMedicine.Filters;
using DigitalMedicine.Models.Users;

namespace DigitalMedicine.Controllers
{
    [MyAuth]
    public class CabinetController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        public ActionResult Index()
        {
            User user = (User)Session["currentUser"];
            return View(user);
        }

        [HttpPost]
        [ExceptionLogger]
        public async Task<ActionResult> ChangeEmail(string oldEmail, string newEmail, string newEmailRepeat)
        {
            if (newEmail != newEmailRepeat)
            {
                ViewBag.Error = "E-mail'ы не совпадают";
                return View("Error");
            }  
            User user = (User)Session["currentUser"];
            if (user.Email != oldEmail)
            {
                ViewBag.Error = "Старый E-mail введен неверно!";
                return View("Error");
            }
            user.Email = newEmail;
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();
            Session["currentUser"] = user;
            return Redirect("Index");     
        }

        [ExceptionLogger]
        public async Task<ActionResult> ChangePassword(string oldpassword, string newPassword, string newPasswordRepeat)
        {
            if (newPassword != newPasswordRepeat)
            {
                ViewBag.Error = "Пароли не совпадают";
                return View("Error");
            }
            User user = (User)Session["currentUser"];
            if (user.Password != oldpassword)
            {
                ViewBag.Error = "Старый пароль введен неверно!";
                return View("Error");
            }
            user.Password = newPassword;
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();
            Session["currentUser"] = user;
            return Redirect("Index");
        }

        [HttpPost]
        [ExceptionLogger]
        public async Task<ActionResult> ChangePhoto(HttpPostedFileBase newPhoto)
        {
            if (newPhoto == null)
            {
                ViewBag.Error = "Фотография не загружена!";
                return View("Error");
            }
            else
            {
                User user = (User)Session["currentUser"];
                byte[] b = new byte[newPhoto.ContentLength];
                await newPhoto.InputStream.ReadAsync(b, 0, newPhoto.ContentLength);
                user.Photo = b;
                user.cutPhoto();                
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                Session["currentUser"] = user;
                return Redirect("Index");
            }
        }

        public ActionResult Logout()
        {
            Session["currentUser"] = null;
            return Redirect("/Home/Index");
        }

        public async Task<string> ConfirmEmail(int id, string email)
        {
            Patient p = (Patient)Session["currentUser"];
            if (p.Id == id && p.Email == email)
            {
                if (p.ConfirmedEmail == true)
                    return "Регистрация уже была подтверждена!";
                else
                {
                    p = await db.Patients.Include(us=>us.Role).Where(us=>us.Id==p.Id).FirstAsync();
                    p.ConfirmedEmail = true;
                    await db.SaveChangesAsync();
                    Session["currentUser"] = p;
                    return "Регистрация подтверждена успешно!";
                }
            }
            else
                return "Вы подтверждаете чужой аккаунт!";
        }

        [HttpPost]
        public async Task<EmptyResult> AddComment([Bind(Exclude ="PublicationTime")] Comment comment)
        {
            comment.PublicationTime = DateTime.Now;
            db.Comments.Add(comment);
            await db.SaveChangesAsync();
            return new EmptyResult();
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