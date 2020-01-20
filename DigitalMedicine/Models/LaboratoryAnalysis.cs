using DigitalMedicine.Models.Institution;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalMedicine.Models
{
    public class LaboratoryAnalysis
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [MaxLength(40)]
        [Display(Name = "Название анализа")]
        [DataType(DataType.Text)]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 40 символов")]
        public string Name { get; set; }

        [MaxLength(500)]
        [Display(Name = "Об анализе")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, MinimumLength = 0, ErrorMessage = "Длина строки должна быть от 10 до 500 символов")]
        public string About { get; set; }

        [Display(Name = "Стоимость")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Время сдачи анализа")]
        [DataType(DataType.Time)]
        public TimeSpan ReceptionTime { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? IdLaboratory { get; set; }
        [ForeignKey("IdLaboratory")]
        public virtual Labaratory Laboratory { get; set; }

        public virtual WorkTime WorkTime { get; set; }

        public virtual ICollection<LaboratoryAppointment> BusyTimes{ get; set; }
    }
}