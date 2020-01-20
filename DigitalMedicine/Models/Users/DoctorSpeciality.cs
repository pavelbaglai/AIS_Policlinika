using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DigitalMedicine.Models.Users
{
    public class DoctorSpeciality
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [MaxLength(30)]
        [Display(Name = "Название специальности")]
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 30 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [MaxLength(500)]
        [Display(Name = "Информация о докторской специальности")]
        [DataType(DataType.Text)]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Длина строки должна быть от 10 до 500 символов")]
        public string Information { get; set; }

        public bool AbilityToPatientRecord { get; set; } = false;

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}