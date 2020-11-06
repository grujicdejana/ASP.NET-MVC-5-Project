 using System.Web;
using System.Web.Mvc;

namespace VidlyProject
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute()); //ovaj filter redirektuje korisnika na error stranicu
            filters.Add(new AuthorizeAttribute());
        }
    }
}
