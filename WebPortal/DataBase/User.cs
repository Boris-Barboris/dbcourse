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
    
    public partial class User
    {
        public User()
        {
            this.passwordChanged = false;
            this.Cancellation = new HashSet<Cancellation>();
            this.Reservation = new HashSet<Reservation>();
        }
    
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public bool passwordChanged { get; set; }
        public string EMail { get; set; }
    
        public virtual ICollection<Cancellation> Cancellation { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
