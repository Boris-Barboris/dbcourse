//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebPortal.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class FlightDetails
    {
        public string FlightNo { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public System.DateTime DepTime { get; set; }
        public System.DateTime ArrTime { get; set; }
        public string AircraftType { get; set; }
        public short SeatsEco { get; set; }
        public short SeatsBn { get; set; }
        public decimal FareEco { get; set; }
        public decimal FareBn { get; set; }
        public short EcoFree { get; set; }
        public short BnFree { get; set; }
        public decimal FareCollected { get; set; }
    }
}
