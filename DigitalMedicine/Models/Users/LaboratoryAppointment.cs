using DigitalMedicine.Models.Institution;
using DigitalMedicine.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalMedicine.Models
{
    public class LaboratoryAppointment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Дата приема")]
        [DataType(DataType.Date)]
        public DateTime Day { get; set; }

        [Required]
        [Display(Name = "Начало приема")]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Display(Name = "Конец приема")]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int IdLaboratoryAnalysis { get; set; }
        [ForeignKey("IdLaboratoryAnalysis")]
        public virtual LaboratoryAnalysis LaboratoryAnalysis { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int IdPatient { get; set; }
        [ForeignKey("IdPatient")]
        public virtual Patient Patient { get; set; }
    }
}