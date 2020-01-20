using System;
using System.Collections.Generic;
using DigitalMedicine.Models.MedCard;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using ImageProcessor;
using System.Drawing;
using System.Web;

namespace DigitalMedicine.Models.Users
{
    [Bind(Exclude = "Photo")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [MaxLength(25)]
        [Display(Name = "Имя")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 25 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [MaxLength(30)]
        [Display(Name = "Фамилия")]
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 30 символов")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [MaxLength(25)]
        [Display(Name = "Отчество")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 25 символов")]
        public string Patronymic { get; set; }

        [Display(Name = "День Рождения")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [MaxLength(30)]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^((\w+)(\.*)(\w+)+)@((\w+)\.(\w+))+$", ErrorMessage = "Некорректный E-mail")]
        public string Email { get; set; }

        [MaxLength(30)]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Логин")]
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Длина логина быть от 6 до 30 символов")]
        public string Login { get; set; }

        [MaxLength(30)]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 30 символов")]
        public string Password { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Фотография(.jpg)")]
        [HiddenInput(DisplayValue = false)]
        public byte[] Photo { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int IdRole { get; set; }
        [ForeignKey("IdRole")]
        public virtual Role Role { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        //Формирование даты рождения
        public string GetBirthday()
        {
            return $"{Birthday.Day}-{GetMonthName(Birthday.Month)}-{Birthday.Year}";
        }

        //Получение названия месяца по номеру
        public string GetMonthName(int number)
        {
            string name=String.Empty;
            switch (number)
            {
                case 1: name = "Января"; break;
                case 2: name = "Февраля"; break;
                case 3: name = "Марта"; break;
                case 4: name = "Апреля"; break;
                case 5: name = "Мая"; break;
                case 6: name = "Июня"; break;
                case 7: name = "Июля"; break;
                case 8: name = "Августа"; break;
                case 9: name = "Сентября"; break;
                case 10: name = "Октября"; break;
                case 11: name = "Ноября"; break;
                case 12: name = "Декабря"; break;
                default: throw new Exception("Неверный номер месяца!");
            }
            return name;
        }

        //Формаирование ФИО (полного или нет)
        public string GetFio(bool full=true)
        {
            string fio;
            if (full)
                fio = $"{Surname} {Name} {Patronymic}";
            else
                fio = $"{Surname} {Name[0]}. {Patronymic[0]}.";
            return fio;
        }

        public void SetPhoto(HttpPostedFileBase photo)
        {
            if (photo != null)
            {
                byte[] imageData = new byte[photo.ContentLength];
                photo.InputStream.ReadAsync(imageData, 0, photo.ContentLength);
                this.Photo = imageData;
                this.cutPhoto();
            }
        }

     //Сжатие фотографии при загрузке
        public void cutPhoto()
        {
            using (ImageFactory image = new ImageFactory(false))
            {
                image.Load(this.Photo);
                if (image.Image.Width > 240)
                {
                    double koefficient = (double)image.Image.Height / image.Image.Width;
                    using (var ms = new MemoryStream())
                    { 
                        image.Resize(new Size(240, (int)Math.Truncate(240 * koefficient))).Save(ms);
                        this.Photo = ms.ToArray();
                    }
                }
            }
        }
    }
}