﻿using System.Globalization;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Validation.Providers;

namespace AngularUI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            // disable [Required] error
            GlobalConfiguration.Configuration.Services.RemoveAll(
                typeof(System.Web.Http.Validation.ModelValidatorProvider),
                v => v is InvalidModelValidatorProvider);

            RegisterWebApiConfig(GlobalConfiguration.Configuration);

        }

        private static void RegisterWebApiConfig(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("DefaultApi",
                                       "api/{controller}/{action}/{id}",
                                       new { id = RouteParameter.Optional, id2 = RouteParameter.Optional }
                );

            //GlobalConfiguration.Configuration.Filters.Add(new AuthorizeAttribute());

            // remove default xml formatter
            var appXmlType =
                config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}