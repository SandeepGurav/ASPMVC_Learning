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
    
    public partial class City
    {
        public City()
        {
            this.Users = new HashSet<User>();
        }
    
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int CountryID { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}