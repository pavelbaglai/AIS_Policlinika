using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DigitalMedicine.Models.Users;

namespace DigitalMedicine.Models
{
    public class Comment
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [MaxLength(200)]
        [Display(Name = "Комментарий")]
        [DataType(DataType.MultilineText)]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 200 символов")]
        public string Text{ get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Дата и время публикации")]
        [DataType(DataType.DateTime)]
        public DateTime PublicationTime { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int IdUser { get; set; }
        [ForeignKey("IdUser")]
        public virtual User User { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int IdNews { get; set; }
        [ForeignKey("IdNews")]
        public virtual News.News News { get; set; }
    }
}