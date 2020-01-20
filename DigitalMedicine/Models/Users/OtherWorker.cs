using DigitalMedicine.Models.Institution;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalMedicine.Models.Users
{
    [Table("OtherWorkers")]
    public class OtherWorker : User
    {
        [HiddenInput(DisplayValue = false)]
        public int IdInstitution { get; set; }
        [ForeignKey("IdInstitution")]
        public virtual Institution.Institution Institution{ get; set; }
    }
}