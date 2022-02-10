using EventiApp.Models.Enums;
using EventiApp.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EventiApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ValidateRolsAndUsers();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ValidateRolsAndUsers()
        {
            UserHelper.CheckRole(RolesEnum.Administrator);
            UserHelper.CheckRole(RolesEnum.Guest);
            UserHelper.CheckRole(RolesEnum.Client);
            UserHelper.CheckSuperUser();
        }
    }
}
