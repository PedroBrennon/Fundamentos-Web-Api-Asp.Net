using System.Web;
using System.Web.Mvc;

namespace AT_Pedro_Paiva
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
