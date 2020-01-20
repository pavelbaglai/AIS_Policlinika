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
using DigitalMedicine.Models.Institution;
using DigitalMedicine.Models.Users;
using DigitalMedicine.Models.KnowledgeBase;
using DigitalMedicine.App_Code;
using DigitalMedicine.Filters;
using Newtonsoft.Json;

namespace DigitalMedicine.Controllers
{
    [MyAuth]
    [MyAuthorize(Roles = "1")]
    public class AdminController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        [HttpGet]
        public async Task<ViewResult> AddAdmin()
        {
            List<Role> Roles =await db.Roles.Where(r => r.Name == "Администратор клиники" || r.Name=="Администратор лаборатории").ToListAsync();
            ViewBag.Roles = Roles;
            List<Institution> institutions = await db.Institutions.ToListAsync();
            ViewBag.Institutions = institutions;
            return View();
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> AddAdmin([Bind(Exclude = "Password")]OtherWorker u, HttpPostedFileBase photo)
        {
            string tempPassword = "temppassword" ;
            u.Password = tempPassword;
            u.SetPhoto(photo);
            db.OtherWorkers.Add(u);
            await db.SaveChangesAsync();
            MailSender send = new MailSender(u.Email);
            send.SendMessageForConfirmAccount(Url.Action("ConfirmEmail", "Cabinet", new { id = u.Id, email = u.Email }, Request.Url.Scheme), tempPassword);
            return RedirectToAction("Index", "Cabinet");
        }

        [HttpGet]
        public async Task<ActionResult> RemoveClinic()
        {
            List<Clinic> clinics = await db.Clinics.ToListAsync();
            return View(clinics);
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> RemoveClinic(int idClinic)
        {
            Clinic clinic = await db.Clinics.FindAsync(idClinic);
            db.OtherWorkers.RemoveRange(clinic.OtherWorkers);
            foreach (var doctor in clinic.Doctors.ToList())
            {
                doctor.IdClinic = null;
                db.Appointments.RemoveRange(db.Appointments.Where(app => app.Doctor.Id == doctor.Id));
                db.WorkTimes.Remove(doctor.WorkTime);
            }
            foreach (var clinicNews in clinic.News.ToList())
            {
                db.Comments.RemoveRange(clinicNews.Comments);
                db.ClinicNews.Remove(clinicNews);
            }
            db.Clinics.Remove(clinic);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Cabinet");
        }

        [HttpGet]
        public async Task<ActionResult> RemoveLabaratory()
        {
            List<Labaratory> labaratories = await db.Labaratories.ToListAsync();
            return View(labaratories);
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> RemoveLabaratory(int idLaboratory)
        {
            Labaratory labaratory = await db.Labaratories.FindAsync(idLaboratory);
            db.OtherWorkers.RemoveRange(labaratory.OtherWorkers);
            foreach (var analysis in labaratory.LaboratoryAnalyses.ToList())
            {
                db.WorkTimes.Remove(analysis.WorkTime);
                db.LaboratoryAnalyses.Remove(analysis);
            }
            foreach (var report in labaratory.LaboratoryReports)
                report.LabaratoryId = null;
            db.Labaratories.Remove(labaratory);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Cabinet");
        }

        [HttpGet]
        public async Task<ActionResult> RemovePharmacy()
        {
            List<Pharmacy> pharmacies= await db.Pharmacies.ToListAsync();
            return View(pharmacies);
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> RemovePharmacy(int idPharmacy)
        {
            Pharmacy pharmacy = await db.Pharmacies.FindAsync(idPharmacy);
            db.Pharmacies.Remove(pharmacy);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Cabinet");
        }

        [HttpGet]
        public ActionResult AddInstitutions()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> AddInstitution(Institution ins, string type)
        {
            switch (type)
            {
                case "clinic": db.Clinics.Add(ins.ConvertToClinic()); break;
                case "laboratory": db.Labaratories.Add(ins.ConvertToLabaratory()); break;
                case "pharmacy": db.Pharmacies.Add(ins.ConvertToPharmacy()); break;
            }
            await db.SaveChangesAsync();
            return "OKey!";
        }

        //Пересчет весов симптомов для определения диагноза
        [HttpGet]
        public async Task<RedirectToRouteResult> RedistributionOfWeights()
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.SymptomsWeights.RemoveRange(db.SymptomsWeights.Where(sw => true));  //удаление всех записей таблицы

                    //Проход по всем диагнозам в БД
                    foreach (var diagnosis in db.Diagnoses.OrderBy(d => d.Id))
                    {
                        uint countSymptoms = 0;
                        Dictionary<int, int> countSymptomsInDiagnosis = new Dictionary<int, int>();

                        //Подсчет кол-ва встречающихся симптомов в диагнозе
                        foreach (var report in db.DoctorReports.Where(r => r.DiagnosisId == diagnosis.Id))
                        {
                            List<int> idSymptoms = Symptom.GetSymptomsInt(report.IdSymptoms);
                            foreach (var idSymptom in idSymptoms)
                            {
                                if (!countSymptomsInDiagnosis.ContainsKey(idSymptom))
                                    countSymptomsInDiagnosis[idSymptom] = 0;
                                countSymptomsInDiagnosis[idSymptom]++;
                                countSymptoms++;
                            }
                        }
                        if (countSymptoms <= 0) continue;
                        double koef = 100.0 / countSymptoms; //коэффициент важности встречи симптома
                        Dictionary<int, double> weightsSymptoms = new Dictionary<int, double>();
                        foreach (var key in countSymptomsInDiagnosis.Keys)
                            weightsSymptoms[key] = countSymptomsInDiagnosis[key] * koef;
                        db.SymptomsWeights.AddRange(GetSymptomWeightsByDiagnosis(diagnosis.Id, weightsSymptoms));
                    }
                    await db.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            return RedirectToAction("Index", "Cabinet");
        }

        //формирование списка весов симптомов для одного диагноза
        private List<SymptomWeight> GetSymptomWeightsByDiagnosis(int idDiagnosis, Dictionary<int, double> weights)
        {
            List<SymptomWeight> symptomsWeights = new List<SymptomWeight>();
            foreach (var key in weights.Keys)
                symptomsWeights.Add(new SymptomWeight() { IdDiagnosis = idDiagnosis, IdSymptom = key, Weight = weights[key] });
            return symptomsWeights;
        }

        [HttpGet]
        public ActionResult SymptomWeights()
        {
            DataChart.Dataset dataset1 = new DataChart.Dataset();
            dataset1.label = "";
            DataChart dc = new DataChart();
            dc.datasets = new List<DataChart.Dataset>() { dataset1 };
            ViewBag.dataChart = JsonConvert.SerializeObject(dc);
            return View();
        }

        public async Task<string> GetSymptomWeightsByDiagnosis(int idDiagnosis)
        {
            Random rand = new Random();
            DataChart dc = new DataChart();
            DataChart.Dataset dataset1 = new DataChart.Dataset();
            List<SymptomWeight> symptomWeights = await db.SymptomsWeights.Where(sw => sw.IdDiagnosis == idDiagnosis).ToListAsync();
            foreach (var symptomWeight in symptomWeights)
            {
                dc.labels.Add(symptomWeight.Symptom.Name);
                dataset1.data.Add(Math.Round(symptomWeight.Weight,2));
                dataset1.backgroundColor.Add($"rgba({rand.Next(0, 255)},{rand.Next(0, 255)},{rand.Next(0, 255)},1)");
                dataset1.borderColor.Add($"rgba({rand.Next(0, 255)},{rand.Next(0, 255)},{rand.Next(0, 255)},0.4)");
            }
            if (symptomWeights.Count != 0)
            {
                dataset1.label = symptomWeights[0].Diagnosis.Name;
                dc.datasets.Add(dataset1);
            }
            else
                dc = null;
            return JsonConvert.SerializeObject(dc);
        }

        [HttpGet]
        public ActionResult DiagnosesBySymptom()
        {
            DataChart.Dataset dataset1 = new DataChart.Dataset();
            dataset1.label = "";
            DataChart dc = new DataChart();
            dc.datasets = new List<DataChart.Dataset>() { dataset1 };
            ViewBag.dataChart = JsonConvert.SerializeObject(dc);
            return View();
        }

        public async Task<string> GetDiagnosesBySymptom(int idSymptom)
        {
            Random rand = new Random();
            DataChart dc = new DataChart();
            DataChart.Dataset dataset1 = new DataChart.Dataset();
            List<SymptomWeight> diagnosesWeights = await db.SymptomsWeights.Where(sw => sw.IdSymptom == idSymptom).ToListAsync();
            foreach (var diagnosisWeight in diagnosesWeights)
            {
                dc.labels.Add(diagnosisWeight.Diagnosis.Name);
                dataset1.data.Add(Math.Round(diagnosisWeight.Weight,2));
                dataset1.backgroundColor.Add($"rgba({rand.Next(0, 255)},{rand.Next(0, 255)},{rand.Next(0, 255)},1)");
                dataset1.borderColor.Add($"rgba({rand.Next(0, 255)},{rand.Next(0, 255)},{rand.Next(0, 255)},0.4)");
            }
            if (diagnosesWeights.Count != 0)
            {
                dataset1.label = diagnosesWeights[0].Symptom.Name;
                dc.datasets.Add(dataset1);
            }
            else
                dc = null;
            return JsonConvert.SerializeObject(dc);
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
