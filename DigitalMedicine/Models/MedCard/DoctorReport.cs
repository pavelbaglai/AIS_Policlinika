using DigitalMedicine.Models.Users;
using DigitalMedicine.Models.KnowledgeBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DigitalMedicine.Models.MedCard
{
    public class DoctorReport
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Дата")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [MaxLength(400)]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Назначение врача")]
        [DataType(DataType.MultilineText)]
        [StringLength(400, MinimumLength = 20, ErrorMessage = "Длина строки должна быть от 20 до 400 символов")]
        public string Appointment { get; set; }

        [MaxLength(200)]
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        [StringLength(200, MinimumLength = 20, ErrorMessage = "Длина строки должна быть от 20 до 200 символов")]
        public string Description { get; set; }

        [MaxLength(300)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Рекоммендации")]
        [StringLength(300, MinimumLength = 20, ErrorMessage = "Длина строки должна быть от 20 до 300 символов")]
        public string Recommendation{ get; set; }

        [MaxLength(10)]
        [DataType(DataType.Text)]
        [Display(Name = "Измеренная температура")]
        public string Temperature{ get; set; }

        [MaxLength(10)]
        [DataType(DataType.Text)]
        [Display(Name = "Измеренное давление")]
        public string Pressure { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Жалобы")]
        public string Complaints { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Жалобы во время приема")]
        public string Complaints2 { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Рекомендованные анализы")]
        public string Analyses { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        [MaxLength(400)]
        [Display(Name = "Симптомы")]
        [DataType(DataType.MultilineText)]
        [StringLength(400, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 400 символов")]
        public string IdSymptoms { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int DiagnosisId { get; set; }
        [ForeignKey("DiagnosisId")]
        public virtual Diagnosis Diagnosis { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor{ get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PatientId{ get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }
    }
}