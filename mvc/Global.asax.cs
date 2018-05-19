using ceya.Infrastructure.DataAccess;
using mvc.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //if (!System.Diagnostics.EventLog.SourceExists("Ceya MVC Framework"))
            //{
            //    System.Diagnostics.EventLog.CreateEventSource("Ceya MVC Framework", "Application");
            //}

            //System.Diagnostics.EventLog.WriteEntry("Ceya MVC Framework",
            //                "Starting Ceya MVC..." + System.Environment.NewLine, System.Diagnostics.EventLogEntryType.Information, 1, short.MaxValue);

            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // BundleTable.EnableOptimizations = true;

            System.Data.Entity.Database.SetInitializer(new SampleData());
            Bootstrapper.Run();
        }

        protected void Application_BeginRequest()
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();

            culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
