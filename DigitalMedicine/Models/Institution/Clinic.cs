using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DigitalMedicine.Models.Users;
using DigitalMedicine.Models.News;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalMedicine.Models.Institution
{
    [Table("Clinics")]
    public class Clinic : Institution
    {

        [JsonIgnore]
        public virtual ICollection<Doctor> Doctors { get; set; }

        [JsonIgnore]
        public virtual ICollection<ClinicNews> News { get; set; }
    }
}