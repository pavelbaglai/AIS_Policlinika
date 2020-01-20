using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DigitalMedicine.Models.News
{
    [Bind(Exclude ="Photo")]
    public abstract class News
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [MaxLength(300)]
        [Display(Name = "Заголовок")]
        [DataType(DataType.Text)]
        [StringLength(300, MinimumLength = 10, ErrorMessage = "Длина строки должна быть от 10 до 300 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [MaxLength(500)]
        [Display(Name = "Сокращенная новость")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "Длина строки должна быть от 20 до 500 символов")]
        public string MiniNews{ get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [MaxLength(1500)]
        [Display(Name = "Полная новость")]
        [DataType(DataType.MultilineText)]
        [StringLength(1500, MinimumLength = 40, ErrorMessage = "Длина строки должна быть от 40 до 1500 символов")]
        public string FullNews { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Дата и время публикации")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Фотография(.jpg)")]
        [HiddenInput(DisplayValue = false)]
        public byte[] Photo { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}