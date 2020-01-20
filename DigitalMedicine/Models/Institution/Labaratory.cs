using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DigitalMedicine.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using DigitalMedicine.Models.MedCard;
using Newtonsoft.Json;

namespace DigitalMedicine.Models.Institution
{
    [Table("Labaratories")]
    public class Labaratory : Institution
    {
        [JsonIgnore]
        public virtual ICollection<LaboratoryReport> LaboratoryReports {get;set;}

        [JsonIgnore]
        public virtual ICollection<LaboratoryAnalysis> LaboratoryAnalyses { get; set; }
    }
}