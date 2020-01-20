using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DigitalMedicine.Models.KnowledgeBase
{
    public class SymptomWeight
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int IdSymptom { get; set; }
        [ForeignKey("IdSymptom")]
        public virtual Symptom Symptom { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int IdDiagnosis { get; set; }
        [ForeignKey("IdDiagnosis")]
        public virtual Diagnosis Diagnosis { get; set; }

        [Required(ErrorMessage = "Вес симптома должен быть установлен!")]
        public double Weight { get; set; }
    }
}