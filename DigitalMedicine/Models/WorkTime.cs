using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DigitalMedicine.Models.Institution;
using DigitalMedicine.Models.Users;

namespace DigitalMedicine.Models
{
    public class WorkTime
    {
        enum TimeMode { Start, End};

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int Id{ get; set; }

        [Required(ErrorMessage = "Заполните время работы в понедельник или поставьте прочерк (-)")]
        [MaxLength(11)]
        [Display(Name = "Понедельник")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^(([0-1][0-9]|[2][0-3]):[0-5][0-9]-([0-1][0-9]|[2][0-3]):[0-5][0-9])|-$", ErrorMessage = "Понедельник указан в неверном формате!")]
        public string Monday { get; set; } = "-";

        [Required(ErrorMessage = "Заполните время работы во вторник или поставьте прочерк (-)")]
        [MaxLength(11)]
        [Display(Name = "Вторник")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^(([0-1][0-9]|[2][0-3]):[0-5][0-9]-([0-1][0-9]|[2][0-3]):[0-5][0-9])|-$", ErrorMessage = "Вторник указан в неверном формате!")]
        public string Tuesday { get; set; } = "-";

        [Required(ErrorMessage = "Заполните время работы в среду или поставьте прочерк (-)")]
        [MaxLength(11)]
        [Display(Name = "Среда")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^(([0-1][0-9]|[2][0-3]):[0-5][0-9]-([0-1][0-9]|[2][0-3]):[0-5][0-9])|-$", ErrorMessage = "Среда указана в неверном формате!")]
        public string Wednesday { get; set; } = "-";

        [Required(ErrorMessage = "Заполните время работы в четверг или поставьте прочерк (-)")]
        [MaxLength(11)]
        [Display(Name = "Четверг")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^(([0-1][0-9]|[2][0-3]):[0-5][0-9]-([0-1][0-9]|[2][0-3]):[0-5][0-9])|-$", ErrorMessage = "Четверг указан в неверном формате!")]
        public string Thursday { get; set; } = "-";

        [Required(ErrorMessage = "Заполните время работы в пятницу или поставьте прочерк (-)")]
        [MaxLength(11)]
        [Display(Name = "Пятница")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^(([0-1][0-9]|[2][0-3]):[0-5][0-9]-([0-1][0-9]|[2][0-3]):[0-5][0-9])|-$", ErrorMessage = "Пятница указана в неверном формате!")]
        public string Friday { get; set; } = "-";

        [Required(ErrorMessage = "Заполните время работы в субботу или поставьте прочерк (-)")]
        [MaxLength(11)]
        [Display(Name = "Суббота")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^(([0-1][0-9]|[2][0-3]):[0-5][0-9]-([0-1][0-9]|[2][0-3]):[0-5][0-9])|-$", ErrorMessage = "Суббота указана в неверном формате!")]
        public string Saturday { get; set; } = "-";

        [Required(ErrorMessage = "Заполните время работы в воскресенье или поставьте прочерк (-)")]
        [MaxLength(11)]
        [Display(Name = "Воскресенье")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^(([0-1][0-9]|[2][0-3]):[0-5][0-9]-([0-1][0-9]|[2][0-3]):[0-5][0-9])|-$", ErrorMessage = "Воскресенье указано в неверном формате!")]
        public string Sunday { get; set; } = "-";

        public TimeSpan? GetStartTime(DayOfWeek day)
        {
            return GetTime(day,TimeMode.Start);
        }

        public TimeSpan? GetEndTime(DayOfWeek day)
        {
            return GetTime(day, TimeMode.End);
        }

        private TimeSpan? GetTime(DayOfWeek day, TimeMode mode)
        {
            string workTime=String.Empty;
            switch (day)
            {
                case DayOfWeek.Monday: workTime = Monday; break;
                case DayOfWeek.Tuesday: workTime = Tuesday; break;
                case DayOfWeek.Wednesday: workTime = Wednesday; break;
                case DayOfWeek.Thursday: workTime = Thursday; break;
                case DayOfWeek.Friday: workTime = Friday; break;
                case DayOfWeek.Saturday: workTime = Saturday; break;
                case DayOfWeek.Sunday: workTime = Sunday; break;
                default: throw new Exception("День указан неверно!");
            }

            TimeSpan? time;
            if (workTime == "-")
                return null;
            string[] digits = workTime.Split(':','-');
            if (mode == TimeMode.Start)
                time = new TimeSpan(int.Parse(digits[0]), int.Parse(digits[1]), 0);
            else
                time = new TimeSpan(int.Parse(digits[2]), int.Parse(digits[3]), 0);
            return time;
        }
    }
}