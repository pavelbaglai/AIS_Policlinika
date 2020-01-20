using System;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalMedicine.Models
{
    public class ExceptionDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Сообщение исключения")]
        [DataType(DataType.Text)]
        public string ExceptionMessage { get; set; }

        [Display(Name = "Имя контроллера")]
        [DataType(DataType.Text)]
        public string ControllerName { get; set; }

        [Display(Name = "Имя действия")]
        [DataType(DataType.Text)]
        public string ActionName { get; set; }

        [Display(Name = "Стек исключения")]
        [DataType(DataType.Text)]
        public string StackTrace { get; set; }

        [Display(Name = "Дата и время возникновения")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }
}