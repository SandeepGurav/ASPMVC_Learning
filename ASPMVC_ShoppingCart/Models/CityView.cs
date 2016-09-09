using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMVC_ShoppingCart.Models
{
    public class CityView
    {
        [Key]
        public int CityID { get; set; }
        [Required]
        public string CityName { get; set; }
        public int CountryID { get; set; }

        public virtual Country Country { get; set; }
    }
}