using ASPMVC_ShoppingCart.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ASPMVC_ShoppingCart
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new CustomAuthorizeFilter());
        }
    }
}
