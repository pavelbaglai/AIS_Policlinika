using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using DigitalMedicine.Models.Institution;

namespace DigitalMedicine.Models.News
{
    [Table("ClinicNews")]
    public class ClinicNews : News
    {
        [HiddenInput(DisplayValue = false)]
        public int IdClinic { get; set; }
        [ForeignKey("IdClinic")]
        public virtual Clinic Clinic { get; set; }
    }
}