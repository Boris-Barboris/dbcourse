using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPortal.DataBase
{

    [MetadataType(typeof(ReservationMetadata))]
    public partial class Reservation
    {
        public class ReservationMetadata
        {
            [Key]
            [Required]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Editable(false)]
            [Display(Name="Номер билета")]
            public int TicketNo { get; set; }

            [Editable(false)]
            [Display(Name = "Номер рейса")]
            [Required(AllowEmptyStrings = false)]
            [MaxLength(20, ErrorMessage = "Максимальная длина номера рейса - 20")]
            [RegularExpression("^[0-9,a-z,A-Z]+$", ErrorMessage = "Номер рейса должен содержать лишь латинские буквы и цифры")]
            public string FlightNo { get; set; }

            //[Editable(false, AllowInitialValue = true)]
            [Display(Name = "Номер паспорта")]
            [Required(AllowEmptyStrings = false)]
            [InverseProperty("Passenger")]
            [MaxLength(50, ErrorMessage = "Максимальная длина номера паспорта - 50")]
            public string ReservedBy { get; set; }

            [Display(Name = "Класс")]
            [Required]
            public FlightClass ClassOfRes { get; set; }

            [Display(Name = "Цена")]
            [Required]
            [Range(typeof(decimal), "0", "999999999999999")]
            [DisplayFormat(DataFormatString = "{0:C2}")]
            [Editable(false)]
            public decimal Fare { get; set; }

            [Display(Name = "Дата покупки")]
            [Required]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
            [Editable(false)]
            public System.DateTime DateOfRes { get; set; }

            [Display(Name = "Статус")]
            [Required]
            [Editable(false)]
            public ReservationStatus Status { get; set; }

            [Display(Name = "e-mail")]
            //[Required(AllowEmptyStrings = false)]
            [StringLength(50, ErrorMessage = "E-Mail не должен быть длиннее 50 символов")]
            public string EMail { get; set; }

            [ForeignKey("FlightNo")]
            public virtual FlightDetails Flight { get; set; }

            [InverseProperty("TicketNo")]
            public virtual Cancellation Cancellation { get; set; }

            [ForeignKey("ReservedBy")]
            public virtual Passenger Passenger { get; set; }
        }
    }
}