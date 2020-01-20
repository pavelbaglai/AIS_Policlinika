using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;
using DigitalMedicine.Models.Users;

namespace DigitalMedicine.Models.Institution
{
    [Bind(Exclude = "Photo")]
    public class Institution
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [JsonIgnore]
        [Display(Name = "Об учреждении")]
        [DataType(DataType.MultilineText)]
        public string About { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Адрес")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [JsonIgnore]
        [DataType(DataType.Upload)]
        [Display(Name = "Фотография(.jpg)")]
        [HiddenInput(DisplayValue = false)]
        public byte[] Photo { get; set; }

        [Display(Name = "Широта")]
        [HiddenInput(DisplayValue = false)]
        public double Latitude { get; set; }

        [Display(Name = "Долгота")]
        [HiddenInput(DisplayValue = false)]
        public double Longitude { get; set; }

        [JsonIgnore]
        public virtual ICollection<OtherWorker> OtherWorkers { get; set; }

        public Clinic ConvertToClinic()
        {
            Clinic clinic = new Clinic() {
                Address=this.Address,
                Name=this.Name,
                Latitude=this.Latitude,
                Longitude=this.Longitude,
                Photo=this.Photo,
                Id=this.Id
            };
            return clinic;
        }
        public Labaratory ConvertToLabaratory()
        {
            Labaratory labaratory = new Labaratory()
            {
                Address = this.Address,
                Name = this.Name,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                Photo = this.Photo,
                Id = this.Id
            };
            return labaratory;
        }
        public Pharmacy ConvertToPharmacy()
        {
            Pharmacy pharmacy = new Pharmacy()
            {
                Address = this.Address,
                Name = this.Name,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                Photo = this.Photo,
                Id = this.Id
            };
            return pharmacy;
        }
    }
}