using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalMedicine.Models.MedCard;

namespace DigitalMedicine.Models.Users
{
    [Table("Patients")]
    public class Patient:User
    {
        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.Text)]
        public bool ConfirmedEmail { get; set; } = false;

        [HiddenInput(DisplayValue = false)]
        public int? IdFamilyDoctor { get; set; }
        [ForeignKey("IdFamilyDoctor")]
        public virtual Doctor FamilyDoctor { get; set; }

        public virtual ICollection<DoctorReport> DoctorReports { get; set; }

        public virtual ICollection<LaboratoryReport> LabaratoryReports { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}