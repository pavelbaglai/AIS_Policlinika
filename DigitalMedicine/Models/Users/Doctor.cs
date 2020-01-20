using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DigitalMedicine.Models.Institution;
using DigitalMedicine.Models.MedCard;

namespace DigitalMedicine.Models.Users
{
    [Table("Doctors")]
    public class Doctor : User
    {
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [MaxLength(800)]
        [Display(Name = "Информация")]
        [DataType(DataType.MultilineText)]
        [StringLength(800,MinimumLength =80, ErrorMessage = "Длина строки должна быть от 40 до 400 символов")]
        public string Information { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Время для приёма пациента")]
        [DataType(DataType.Time)]
        public TimeSpan ReceptionTime { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Время чередования электронной и живой очереди")]
        [DataType(DataType.Time)]
        public TimeSpan RotationTime { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? IdClinic { get; set; }
        [ForeignKey("IdClinic")]
        public virtual Clinic Clinic{ get; set; }

        public virtual ICollection<Patient> Patients{ get; set; }

        public virtual ICollection<DoctorSpeciality> Specialities{ get; set; }

        public virtual ICollection<Appointment> BusyTimes { get; set; }

        public virtual ICollection<DoctorReport> DoctorReports { get; set; }

        public virtual WorkTime WorkTime { get; set; }
    }
}