using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMVC_ShoppingCart.Models
{
        public class BudgetaryExpdrMetdaData
        {
            [StringLength(50)]
            [Display(Name = "Head of Account")]
            public string HeadAcount { get; set; }

            [Range(10,2) ]
            [Display(Name = "Demand BE in Lakh")]
            public decimal DemandBE { get; set; }

            [Range(10, 2)]
            [Display(Name = "Demand RE in Lakh")]
            public decimal DemandRE { get; set; }

            [Range(10, 2)]
            [Display(Name = "Upto last month")]
            public decimal ExpdrLastMonth { get; set; }

            [Range(10, 2)]
            [Display(Name = "During the month")]
            public decimal ExpdrDuringMonth { get; set; }

            [Range(10, 2)]
            [Display(Name = "Total during the year")]
            public decimal ExpdrTotalYear { get; set; }

            [Range(10, 2)]
            [Display(Name = "%Expdr over allotment")]
            public decimal ExpdrOverAllotment { get; set; }
           
            [StringLength(50)]
            [Display(Name = "CE Zone")]
            public string CEZone { get; set; }

            [StringLength(20)]
            [Display(Name = "Month")]
            public string Month { get; set; }

            [StringLength(30)]
            [Display(Name = "Year")]
            public int Year { get; set; }
        }

        public  class CountryMetaData
        {
            [StringLength(30)]
            [Display(Name = "Name of Country")]
            public string CountryName { get; set; }
        }

   
}