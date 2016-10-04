using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPMVC_ShoppingCart.Security
{
    public static class SessionPersister
    {
        public static string usernamesessionvar = "EmailID";
        public static string EmailID
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var SessionVar = HttpContext.Current.Session[usernamesessionvar];
                if (SessionVar!=null)
                    return SessionVar as String;
                return null;
            }
            set
            {
                HttpContext.Current.Session[usernamesessionvar] = value;
            }
        }

    }
}