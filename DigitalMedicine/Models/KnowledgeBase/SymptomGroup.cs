using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DigitalMedicine.Models.KnowledgeBase
{
    public class SymptomGroup
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [MaxLength(25)]
        [Display(Name = "Название группы симптомов")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 25 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [MaxLength(500)]
        [Display(Name = "Информация о группе симптомов")]
        [DataType(DataType.Text)]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Длина строки должна быть от 10 до 500 символов")]
        public string Information { get; set; }

        public virtual ICollection<Symptom> Symptoms { get; set; }
    }
}