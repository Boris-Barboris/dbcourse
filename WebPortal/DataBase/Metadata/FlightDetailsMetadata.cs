using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPortal.DataBase
{
    [MetadataType(typeof(FlightDetailsMetadata))]
    public partial class FlightDetails
    {
        public class FlightDetailsMetadata
        {
            [Key]
            [Required(AllowEmptyStrings = false)]
            [MaxLength(20, ErrorMessage = "Максимальная длина номера рейса - 20")]
            [RegularExpression("^[0-9,a-z,A-Z]+$", ErrorMessage = "Номер рейса должен содержать лишь латинские буквы и цифры")]
            [Display(Name = "Номер рейса")]
            public string FlightNo { get; set; }

            [Required(AllowEmptyStrings = false)]
            [Display(Name = "Вылет из")]
            public string Origin { get; set; }

            [Required(AllowEmptyStrings = false)]
            [Display(Name = "Пункт назначения")]
            public string Destination { get; set; }

            [Required]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
            [Display(Name = "Дата и время отправления")]
            public System.DateTime DepTime { get; set; }

            [Required]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
            [Display(Name = "Дата и время прибытия")]
            public System.DateTime ArrTime { get; set; }

            [Required(AllowEmptyStrings = false)]
            [Display(Name = "Тип самолёта")]
            public string AircraftType { get; set; }

            [Required]
            [Range(0, short.MaxValue)]
            [Display(Name = "Мест эконом-класса всего")]
            [DefaultValue(0)]
            public short SeatsEco { get; set; }

            [Required]
            [Range(0, short.MaxValue)]
            [Display(Name = "Мест бизнесс-класса всего")]
            [DefaultValue(0)]
            public short SeatsBn { get; set; }

            [Required]
            [Display(Name = "Цена эконом-класс")]
            [Range(typeof(decimal), "0", "999999999999999")]
            [DisplayFormat(DataFormatString = "{0:C2}")]
            [DefaultValue(0)]
            public decimal FareEco { get; set; }

            [Required]
            [Display(Name = "Цена бизнесс-класс")]
            [Range(typeof(decimal), "0", "999999999999999")]
            [DisplayFormat(DataFormatString = "{0:C2}")]
            [DefaultValue(0)]
            public decimal FareBn { get; set; }

            [Required]
            [Range(0, short.MaxValue)]
            [Display(Name = "Мест эконом-класса свободно")]
            [Editable(false)]
            public short EcoFree { get; set; }

            [Required]
            [Range(0, short.MaxValue)]
            [Display(Name = "Мест бизнес-класса свободно")]
            [Editable(false)]
            public short BnFree { get; set; }

            [Required]
            [Display(Name = "Выручка")]
            [Editable(false)]
            [DisplayFormat(DataFormatString = "{0:C2}")]
            [Range(typeof(decimal), "0", "999999999999999")]
            public decimal FareCollected { get; set; }
        }

    }
}