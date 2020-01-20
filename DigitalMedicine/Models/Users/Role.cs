using DigitalMedicine.Models.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DigitalMedicine.Models
{
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [MaxLength(30)]
        [Display(Name = "Название роли")]
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Длина строки должна быть от 4 до 30 символов")]
        public string Name { get; set; }

        [MaxLength(200)]
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Длина строки должна быть от 10 до 200 символов")]
        public string Description{ get; set; }
    }
}