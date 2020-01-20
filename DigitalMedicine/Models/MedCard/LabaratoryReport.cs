using DigitalMedicine.Models.Institution;
using DigitalMedicine.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DigitalMedicine.Models.MedCard
{
    public class LaboratoryReport
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id{ get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Дата")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }


        [MaxLength(300)]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        [StringLength(300, MinimumLength = 10, ErrorMessage = "Длина строки должна быть от 10 до 300 символов")]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        public byte[] Files { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? LabaratoryId { get; set; }
        [ForeignKey("LabaratoryId")]
        public virtual Labaratory Labaratory { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? IdLaboratoryAnalysis { get; set; }
        [ForeignKey("IdLaboratoryAnalysis")]
        public virtual LaboratoryAnalysis LaboratoryAnalysis { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }
    }
}