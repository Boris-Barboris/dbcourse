using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPortal.DataBase
{

    [MetadataType(typeof(CancellationMetadata))]
    public partial class Cancellation
    {
        public class CancellationMetadata
        {
            [Key]
            [Required]
            public int TicketNo { get; set; }

            [Required]
            [Range(typeof(decimal), "0", "999999999999999")]
            [DisplayFormat(DataFormatString = "0.00")]
            public decimal Refund { get; set; }

            [Required]
            public string UserServiced { get; set; }

            [Required]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
            public System.DateTime CancelDate { get; set; }
            public string Comment { get; set; }

            [ForeignKey("TicketNo")]
            public virtual Reservation Reservation { get; set; }

            [ForeignKey("UserServiced")]
            public virtual User Operator { get; set; }
        }
    }
}