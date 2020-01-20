using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalMedicine.Filters;
using DigitalMedicine.App_Code;
using DigitalMedicine.Models;
using DigitalMedicine.Models.Users;
using DigitalMedicine.Models.Institution;
using DigitalMedicine.Models.News;
using Newtonsoft.Json;

namespace DigitalMedicine.Controllers
{
    public class HomeController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //DataChart.Dataset dataset1 = new DataChart.Dataset();
            //dataset1.label = "Choice a number";
            //DataChart dc = new DataChart();
            //dc.datasets = new List<DataChart.Dataset>() { dataset1 };
            //ViewBag.dataChart = JsonConvert.SerializeObject(dc);
            //return View();

            DataChart.Dataset dataset1 = new DataChart.Dataset();
            dataset1.label = "# of Votes";
            dataset1.data = new List<double>() { 12, 19, 3, 5, 2, 3 };
            dataset1.backgroundColor = new List<string>() {
                "rgba(255, 99, 132, 0.2)",
                "rgba(54, 162, 235, 0.2)",
                "rgba(255, 206, 86, 0.2)",
                "rgba(75, 192, 192, 0.2)",
                "rgba(153, 102, 255, 0.2)",
                "rgba(255, 159, 64, 0.2)"
            };
            dataset1.borderColor = new List<string>(){
                "rgba(255,99,132,1)",
                "rgba(54, 162, 235, 1)",
                "rgba(255, 206, 86, 1)",
                "rgba(75, 192, 192, 1)",
                "rgba(153, 102, 255, 1)",
                "rgba(255, 159, 64, 1)"
            };
            dataset1.borderWidth = 1;
            DataChart dc = new DataChart();
            dc.datasets = new List<DataChart.Dataset>() { dataset1 };
            dc.labels = new List<string>() {
                "Red",
                "Blue",
                "Yellow",
                "Green",
                "Purple",
                "Orange"
            };
            ViewBag.dataChart = JsonConvert.SerializeObject(dc);
            return View();
        }

        [HttpPost]
        public string GetDataForChart(int x)
        {
            DataChart dc = new DataChart();
            DataChart.Dataset dataset1 = new DataChart.Dataset();
            dataset1.backgroundColor = new List<string>() {
                    "rgba(255, 99, 132, 0.2)",
                    "rgba(54, 162, 235, 0.2)",
                    "rgba(255, 206, 86, 0.2)",
                    "rgba(75, 192, 192, 0.2)",
                    "rgba(153, 102, 255, 0.2)",
                    "rgba(255, 159, 64, 0.2)"
                };
            dataset1.borderColor = new List<string>(){
                    "rgba(255,99,132,1)",
                    "rgba(54, 162, 235, 1)",
                    "rgba(255, 206, 86, 1)",
                    "rgba(75, 192, 192, 1)",
                    "rgba(153, 102, 255, 1)",
                    "rgba(255, 159, 64, 1)"
                };
            dataset1.borderWidth = 1;
            if (x == 1)
            {
                dataset1.label = "test1";
                dataset1.data = new List<double>() { 12, 19, 3, 5, 2, 3 };
                dc.labels = new List<string>() {
                    "Red",
                    "Blue",
                    "Yellow",
                    "Green",
                    "Purple",
                    "Orange"
                };
            }
            else
            {
                dataset1.label = "# of Votes";
                dataset1.data = new List<double>() { 5, 9, 13, 1, 8, 7 };
                dc.labels = new List<string>() {
                    "Test",
                    "Blue",
                    "Yellow",
                    "Green",
                    "Purple",
                    "Orange"
                };
            }
            dc.datasets = new List<DataChart.Dataset>() { dataset1 };
            return JsonConvert.SerializeObject(dc);
        }

        public async Task<ActionResult> Map()
        {
            List<Clinic> clinics =await db.Clinics.ToListAsync();
            List<Labaratory> labaratories= await db.Labaratories.ToListAsync();
            List<Pharmacy> pharmacies= await db.Pharmacies.ToListAsync();
            ViewBag.Clinics = JsonConvert.SerializeObject(clinics);
            ViewBag.Labaratories = JsonConvert.SerializeObject(labaratories);
            ViewBag.Pharmacies = JsonConvert.SerializeObject(pharmacies);
            return View();
        }

        [HttpGet]
        public ActionResult Authorization()
        {
            ViewBag.Message = "Авторизация";
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Authorization([Bind(Include = "Login, Password")] User user, string changeRole)
        {
            try
            {
                var currentUser = await db.Users.Include(u => u.Role).SingleAsync(u => u.Login == user.Login && u.Password == user.Password);
                if (currentUser.Role.Name == "Модератор клиники")
                    Session["clinic"] =db.OtherWorkers.Find(currentUser.Id).Institution.Id;
                else if (currentUser.Role.Name == "Администратор лаборатории")
                    Session["laboratory"] = db.OtherWorkers.Find(currentUser.Id).Institution.Id;
                Session["currentUser"] = currentUser;
                return RedirectToAction("Index", "Cabinet");
            }
            catch (InvalidOperationException)
            {
                ViewBag.Error = "Логин или пароль введены некорректно!";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.ToString();
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Registration()
        {
            int idRole = db.Roles.Where(r => r.Name == "Пациент").Single().Id;
            ViewBag.IdRole = idRole;
            ViewBag.Message = "Регситрация пациента";
            return View();
        }

        [HttpPost]
        [ExceptionLogger]
        public async Task<ActionResult> Registration(Patient p, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null)
                {
                    byte[] imageData = new byte[photo.ContentLength];
                    await photo.InputStream.ReadAsync(imageData, 0, photo.ContentLength);
                    p.Photo = imageData;
                    p.cutPhoto();
                }
                db.Patients.Add(p);
                await db.SaveChangesAsync();
                Session["currentUser"] = p;
                MailSender send = new MailSender(p.Email);
                send.SendMessageForConfirmAccount(Url.Action("ConfirmEmail", "Cabinet", new { id = p.Id, email = p.Email }, Request.Url.Scheme));
                return RedirectToAction("Authorization", "Home");
            }
            else
            {
                ViewBag.Error = "Данные введены некорректно!";
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Clinics()
        {
            List<Clinic> clinics = await db.Clinics.OrderBy(c => c.Name).ToListAsync();
            return View(clinics);
        }

        public ActionResult Clinic(int id)
        {
            Clinic clinic = db.Clinics.Find(id);
            return View(clinic);
        }

        public ActionResult News(int id)
        {
            ClinicNews news = db.ClinicNews.Find(id);
            return View(news);
        }

        public async Task<ActionResult> Doctor(int id)
        {
            Doctor doctor = await db.Doctors.FindAsync(id);
            return View(doctor);
        }

        public async Task<ActionResult> Labaratories()
        {
            List<Labaratory> labaratories = await db.Labaratories.ToListAsync();
            return View(labaratories);
        }

        public async Task<ActionResult> Laboratory(int id)
        {
            Labaratory labaratory = await db.Labaratories.FindAsync(id);
            return View(labaratory);
        }

        public async Task<ActionResult> Analysis(int id)
        {
            LaboratoryAnalysis analysis = await db.LaboratoryAnalyses.FindAsync(id);
            return View(analysis);
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