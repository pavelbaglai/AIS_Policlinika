using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DigitalMedicine.Models;
using DigitalMedicine.Filters;
using DigitalMedicine.App_Code;
using System.Data.Entity;
using DigitalMedicine.Models.Users;
using DigitalMedicine.Models.KnowledgeBase;

namespace DigitalMedicine.Controllers
{
    [MyAuth]
    [MyAuthorize(Roles = "4,6")]
    public class AppointmentController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        [HttpGet]
        public async Task<ActionResult> MakeAnAppointment(int id)
        {
            Doctor d = null;
            d = await db.Doctors.FindAsync(id);
            return View(d);
        }

        [HttpPost]
        [ExceptionLogger]
        public async Task<string> MakeAnAppointment(Appointment busyTime, DateTime selectedDate, TimeSpan receptionTime, List<string> symptoms)
        {
            busyTime.PredictedDiagnoses = PredicateDiagnoses(symptoms);
            busyTime.IdSymptoms = Symptom.GetSymptomsString(symptoms);
            busyTime.EndTime = busyTime.StartTime + receptionTime;
            busyTime.Day = new DateTime(DateTime.Now.Year, selectedDate.Month, selectedDate.Day);
            List<Appointment> checkTime = db.Appointments.Where(bt => bt.IdDoctor == busyTime.IdDoctor && bt.StartTime == busyTime.StartTime && bt.Day == busyTime.Day).ToList();
            if (checkTime.Count > 0)
                return "<h3>Время приёма уже занято! Выберите другое время!</h3>";
            List<Appointment> apps = db.Appointments.Where(app => app.IdPatient == busyTime.IdPatient && app.Day == busyTime.Day && !((app.StartTime<busyTime.StartTime && app.EndTime<=busyTime.StartTime)||(app.StartTime>=busyTime.EndTime && app.EndTime>busyTime.EndTime))).ToList<Appointment>();
            if (apps.Count != 0)
                return "<h3>Извините, но на это время вы записаны к другому врачу!</h3>";
            db.Appointments.Add(busyTime);
            await db.SaveChangesAsync();

            User patient = await db.Patients.FindAsync(busyTime.IdPatient);
            User doctor = await db.Doctors.FindAsync(busyTime.IdDoctor);
            MailSender sender = new MailSender(patient.Email);
            sender.SendMessageForMakeAppointment(doctor, selectedDate, busyTime.StartTime, MailSender.UserType.Patient, Url.Action("Doctor", "Home", new { id = busyTime.IdDoctor }, Request.Url.Scheme));
            sender.Recipient = doctor.Email;
            sender.SendMessageForMakeAppointment(patient, selectedDate, busyTime.StartTime, MailSender.UserType.Doctor);
            return $"<h3>Запись была осуществлена успешно!</h3><br/><a href={Url.Action("Index","Cabinet")}>В личный кабинет</a>";
        }

        [HttpGet]
        public async Task<ActionResult> MakeAnAppointmentDoctor(int id)
        {
            Doctor doctor = null;
            doctor = await db.Doctors.FindAsync(id);
            int curDoctorId = ((User)Session["currentUser"]).Id;
            List<Appointment> appointments = await db.Appointments.Where(ap => ap.IdDoctor == curDoctorId).Include(ap => ap.Patient).ToListAsync();
            ViewBag.Appointments = appointments;
            return View(doctor);
        }

        [HttpPost]
        [ExceptionLogger]
        public async Task<string> MakeAnAppointmentDoctor(Appointment busyTime, DateTime selectedDate, TimeSpan receptionTime, string patientId)
        {
            int pId = int.Parse(patientId);
            int dId = ((User)Session["currentUser"]).Id;
            Appointment oldAppointment = db.Appointments.Where(ap => ap.IdDoctor == dId && ap.IdPatient == pId).First();
            busyTime.PredictedDiagnoses = PredicateDiagnoses(oldAppointment.IdSymptoms.Split(';').ToList());
            busyTime.IdSymptoms = oldAppointment.IdSymptoms;
            busyTime.EndTime = busyTime.StartTime + receptionTime;
            busyTime.Day = new DateTime(DateTime.Now.Year, selectedDate.Month, selectedDate.Day);
            busyTime.IdPatient = pId;
            List<Appointment> checkTime = db.Appointments.Where(bt => bt.IdDoctor == busyTime.IdDoctor && bt.StartTime == busyTime.StartTime && bt.Day == busyTime.Day).ToList();
            if (checkTime.Count > 0)
                return "<h3>Время приёма уже занято! Выберите другое время!</h3>";
            List<Appointment> apps = db.Appointments.Where(app => app.IdPatient == busyTime.IdPatient && app.Day == busyTime.Day && !((app.StartTime < busyTime.StartTime && app.EndTime <= busyTime.StartTime) || (app.StartTime >= busyTime.EndTime && app.EndTime > busyTime.EndTime))).ToList<Appointment>();
            if (apps.Count != 0)
                return "<h3>Пациент на это время уже записан к другому врачу!</h3>";
            db.Appointments.Add(busyTime);
            db.Entry(oldAppointment).State = EntityState.Deleted;
            await db.SaveChangesAsync();

            User patient = await db.Patients.FindAsync(busyTime.IdPatient);
            User doctor = await db.Doctors.FindAsync(busyTime.IdDoctor);
            MailSender sender = new MailSender(patient.Email);
            sender.SendMessageForMakeAppointment(doctor, selectedDate, busyTime.StartTime, MailSender.UserType.Patient, Url.Action("Doctor", "Home", new { id = busyTime.IdDoctor }, Request.Url.Scheme));
            sender.Recipient = doctor.Email;
            sender.SendMessageForMakeAppointment(patient, selectedDate, busyTime.StartTime, MailSender.UserType.Doctor);
            return $"<h3>Запись была осуществлена успешно!</h3><br/><a href={Url.Action("Index","Cabinet")}>В личный кабинет</a>";
        }

        //вычисление свободного времени доктора для осуществления записи
        [HttpGet]
        [ExceptionLogger]
        public async Task<PartialViewResult> GetFreeTimes(int idDoctor, DateTime date)
        {
            List<TimeSpan> freeTimes = new List<TimeSpan>();
            Doctor doctor = await db.Doctors.FindAsync(idDoctor);

            //Проверяемое время
            TimeSpan? checkTime = doctor.WorkTime.GetStartTime(date.DayOfWeek),
                endTime = doctor.WorkTime.GetEndTime(date.DayOfWeek);
            if (checkTime == null)
                return PartialView(null);

            int countPatients = int.MaxValue, i = 1;
            //Количество обслуживаемых пациентов из электронной очереди между чередованиями с пациентов из живой очереди
            if (doctor.RotationTime.TotalMinutes != 0)
                countPatients = (int)Math.Truncate((double)doctor.RotationTime.TotalMinutes / doctor.ReceptionTime.TotalMinutes);


            //Получение времени начала записей всех пациентов в данный день к данному врачу
            List<TimeSpan> busyTimes = (from busyTime in doctor.BusyTimes
                                        where busyTime.Day == date
                                        orderby busyTime.StartTime
                                        select busyTime.StartTime).ToList();

            while (checkTime + doctor.ReceptionTime <= endTime)
            {
                if (i > countPatients)
                {
                    checkTime += doctor.RotationTime;
                    i = 1;
                    continue;
                }
                freeTimes.Add((TimeSpan)checkTime);
                checkTime += doctor.ReceptionTime;
                i++;
            }
            //Исключение из свободного времени занятое
            freeTimes = freeTimes.Except(busyTimes).ToList();
            return PartialView(freeTimes);
        }

        //Определить 3 найвероятнейших диагноза по симптомам
        private string PredicateDiagnoses(List<string> idSymptoms)
        {
            string result = String.Empty;
            Dictionary<string, double> probabilityOfDiagnoses = new Dictionary<string, double>();

            //подсчет вероятности всех диагнозов
            foreach (var diagnosis in db.Diagnoses)
            {
                List<SymptomWeight> weights = db.SymptomsWeights.Where(sw => sw.IdDiagnosis == diagnosis.Id).ToList();
                if (weights.Count == 0) continue;
                double probabilityOfDiagnosis = 0;
                foreach (var idSymptom in idSymptoms)
                {
                    var weight = weights.Where(w => w.IdSymptom == int.Parse(idSymptom));
                    if (weight.Count() == 0) continue;
                    probabilityOfDiagnosis += Math.Round(weight.First().Weight, 2);
                }
                probabilityOfDiagnoses.Add(diagnosis.Name, probabilityOfDiagnosis);
            }

            //Формирование строки с 3 найвероятнейшими диагнозами
            foreach (var diagnosis in probabilityOfDiagnoses.OrderByDescending(p => p.Value).Take(3))
                if (diagnosis.Value != 0)
                    result += $"{diagnosis.Key} - {diagnosis.Value}%\n";

            return result;
        }

        //Отмена записи
        public async Task<RedirectToRouteResult> CancelAppointment(int id)
        {
            Appointment busyTime = await db.Appointments.FindAsync(id);
            db.Entry(busyTime).State = EntityState.Deleted;
            await db.SaveChangesAsync();
            return RedirectToAction("Index","Cabinet");
        }

        [HttpGet]
        public async Task<ActionResult> MakeAnAppointmentAnalysis(int id)
        {
            LaboratoryAnalysis analysis=await db.LaboratoryAnalyses.FindAsync(id);
            return View(analysis);
        }

        [HttpPost]
        public async Task<string> MakeAnAppointmentAnalysis(LaboratoryAppointment app, DateTime selectedDate, TimeSpan receptionTime)
        {
            app.Day = selectedDate;
            app.EndTime = app.StartTime + receptionTime;

            List<LaboratoryAppointment> checkTime = db.LaboratoryAppointments.Where(bt => bt.Id== app.Id && bt.StartTime == app.StartTime && bt.Day == app.Day).ToList();
            if (checkTime.Count > 0)
                return "<h3>Время приёма уже занято! Выберите другое время!</h3>";
            List<LaboratoryAppointment> apps = db.LaboratoryAppointments.Where(ap => ap.IdPatient == app.IdPatient && ap.Day == app.Day && !((ap.StartTime < app.StartTime && ap.EndTime <= app.StartTime) || (ap.StartTime >= app.EndTime && ap.EndTime > app.EndTime))).ToList<LaboratoryAppointment>();
            if (apps.Count != 0)
                return "<h3>Извините, но на это время вы записаны на другой анализ!</h3>";
            db.LaboratoryAppointments.Add(app);
            await db.SaveChangesAsync();

            User patient = await db.Patients.FindAsync(app.IdPatient);
            MailSender sender = new MailSender(patient.Email);
            sender.SendMessageForMakeAppointment(patient, selectedDate, app.StartTime, MailSender.UserType.Doctor);
            return $"<h3>Запись была осуществлена успешно!</h3><br/><a href={Url.Action("Index","Cabinet")}>В личный кабинет</a>";
        }

        [HttpGet]
        [ExceptionLogger]
        public async Task<PartialViewResult> GetFreeTimesAnalysis(int idAnalysis, DateTime date)
        {
            List<TimeSpan> freeTimes = new List<TimeSpan>();
            LaboratoryAnalysis analysis = await db.LaboratoryAnalyses.FindAsync(idAnalysis);

            //Проверяемое время
            TimeSpan? checkTime = analysis.WorkTime.GetStartTime(date.DayOfWeek),
                endTime = analysis.WorkTime.GetEndTime(date.DayOfWeek);
            if (checkTime == null)
                return PartialView(null);

            //Получение времени начала записей всех пациентов в данный день к данному врачу
            List<TimeSpan> busyTimes = (from busyTime in analysis.BusyTimes
                                        where busyTime.Day == date
                                        orderby busyTime.StartTime
                                        select busyTime.StartTime).ToList();

            while (checkTime + analysis.ReceptionTime <= endTime)
            {
                freeTimes.Add((TimeSpan)checkTime);
                checkTime += analysis.ReceptionTime;
            }
            //Исключение из свободного времени занятое
            freeTimes = freeTimes.Except(busyTimes).ToList();
            return PartialView("GetFreeTimes", freeTimes);
        }

        public async Task<RedirectToRouteResult> CancelAppointmentAnalysis(int id)
        {
            LaboratoryAppointment app = await db.LaboratoryAppointments.FindAsync(id);
            db.Entry(app).State = EntityState.Deleted;
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