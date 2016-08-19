using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPortal.DataBase
{
    [MetadataType(typeof(PassengerMetadata))]
    public partial class Passenger
    {
        public class PassengerMetadata
        {
            [Key]
            [Required(AllowEmptyStrings = false)]
            [InverseProperty("Passenger")]
            [MaxLength(50, ErrorMessage = "Максимальная длина номера паспорта - 50")]
            [Display(Name="Номер паспорта")]
            public string ID { get; set; }

            [Required(AllowEmptyStrings = false)]
            [Display(Name = "ФИО")]
            public string Name { get; set; }

            [Required]
            [Range(typeof(decimal), "0", "999999999999999")]
			[DisplayFormat(DataFormatString = "{0:C2}")]
            [Display(Name = "Всего оплатил")]
            public decimal FareCollected { get; set; }

            [Required]
            [Range(0, int.MaxValue)]
            [Display(Name = "Кол-во перелётов")]
            public int TotalTimesFlown { get; set; }

            [Required]
            [Range(0.0, 1.0)]
            [Display(Name = "Скидка")]
            public float Discount { get; set; }

            [InverseProperty("Passenger")]
            public virtual ICollection<Reservation> Reservation { get; set; }
        }
    }
}