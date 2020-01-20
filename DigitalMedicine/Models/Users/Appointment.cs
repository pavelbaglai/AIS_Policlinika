using DigitalMedicine.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DigitalMedicine.Models
{
    public class Appointment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int Id{ get; set; }

        [Required(ErrorMessage ="Поле должно быть заполнено!")]
        [MaxLength(400)]
        [Display(Name = "Симптомы")]
        [DataType(DataType.MultilineText)]
        [StringLength(400, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 400 символов")]
        public string IdSymptoms{ get; set; }

        [Display(Name = "Вероятные диагнозы")]
        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        [StringLength(200, MinimumLength = 0, ErrorMessage = "Длина строки должна быть от 0 до 200 символов")]
        public string PredictedDiagnoses { get; set; }

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
        public int IdDoctor { get; set; }
        [ForeignKey("IdDoctor")]
        public virtual Doctor Doctor { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int IdPatient { get; set; }
        [ForeignKey("IdPatient")]
        public virtual Patient Patient { get; set; }
    }
}