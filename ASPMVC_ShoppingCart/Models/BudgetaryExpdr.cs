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
    
    public partial class BudgetaryExpdr
    {
        public int Budg_ID { get; set; }
        public string HeadAcount { get; set; }
        public Nullable<decimal> DemandBE { get; set; }
        public Nullable<decimal> DemandRE { get; set; }
        public Nullable<decimal> ExpdrLastMonth { get; set; }
        public Nullable<decimal> ExpdrDuringMonth { get; set; }
        public Nullable<decimal> ExpdrTotalYear { get; set; }
        public Nullable<decimal> ExpdrOverAllotment { get; set; }
        public string CEZone { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public Nullable<decimal> Alloted { get; set; }
    }
}
