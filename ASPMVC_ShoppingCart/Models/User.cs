//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASPMVC_ShoppingCart.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string ContactNo { get; set; }
        public int CountryID { get; set; }
        public Nullable<int> CityID { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
    }
}
