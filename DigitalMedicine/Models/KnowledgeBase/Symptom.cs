using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DigitalMedicine.Models.KnowledgeBase
{
    public class Symptom
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [MaxLength(80)]
        [Display(Name = "Название симптома")]
        [DataType(DataType.Text)]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 80 символов")]
        public string Name { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int IdSymptomGroup { get; set; }
        [ForeignKey("IdSymptomGroup")]
        public virtual SymptomGroup SymptomGroup{ get; set; }

        public static string GetSymptomsString(List<string> symptomsId)
        {
            string symptomsString = String.Empty;
            if (symptomsId == null)
                return symptomsString;
            foreach (var symptomId in symptomsId)
                symptomsString += $"{symptomId};";
            return symptomsString.Remove(symptomsString.Length-1);
        }

        public static List<int> GetSymptomsInt(string symptomsId)
        {
            List<int> symptomsList = new List<int>();
            if (symptomsId==null || symptomsId == "")
                return symptomsList;
            string[] arr=symptomsId.Split(';');
            foreach (var symptomId in arr)
                symptomsList.Add(int.Parse(symptomId));
            return symptomsList;
        }
    }
}