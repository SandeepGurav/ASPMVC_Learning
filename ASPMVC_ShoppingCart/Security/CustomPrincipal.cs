using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using ASPMVC_ShoppingCart.Models;

namespace ASPMVC_ShoppingCart.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public User User;
        private ASPMVCEntities db = new ASPMVCEntities();
        private Models.User user;

        public CustomPrincipal(string EmailID)
        {
            var um = db.Users.SingleOrDefault(u => u.EmailID == EmailID);
            this.User = um;
            this.Identity = new GenericIdentity(EmailID);
        }

        public IIdentity Identity
        {
            get;
            set;
        }

        public bool IsInRole(string role)
        {
            //var roles = role.Split(new char[] { ',' });
            string emailid = Convert.ToString(SessionPersister.EmailID);
            var un = db.Users.SingleOrDefault(u => u.EmailID.Equals(emailid));
            if (role == un.Role)
            {
                return true;
            }
            return false;
        }
    }
}