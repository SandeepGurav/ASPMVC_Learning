using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using ASPMVC_ShoppingCart.Models;

namespace ASPMVC_ShoppingCart.Security
{
    public class CustomAuthorizeFilter : AuthorizeAttribute
    {
        private ASPMVCEntities db = new ASPMVCEntities();

       

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
           // base.OnAuthorization(filterContext);
            var action = filterContext.ActionDescriptor;

            if (!action.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            //else
           // base.OnAuthorization(filterContext);
            if (string.IsNullOrEmpty(SessionPersister.EmailID))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Users", action = "Login" }));
            }

            else
            {
                //User User = new User();
                string emailidsess = Convert.ToString(SessionPersister.EmailID);
               // UserMaster UM = new UserMaster();
                //User = db.Users.Find(Convert.ToBase64CharArray StiSessionPersister.EmailID);
                //  User User = db.UsersWhere(i => i.EmailID == emailidsess)
                //.Single();
                //var us = UM.Find(Convert.ToString(SessionPersister.EmailID));
                //User=
                //string email = "";
                //email = User.EmailID;
                CustomPrincipal mp = new CustomPrincipal(emailidsess);
                if (!mp.IsInRole(Roles))
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "AccessDenied", Action = "Index" }));

            }

        }
    }
}