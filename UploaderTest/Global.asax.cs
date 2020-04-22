using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Data.Entity;
using UploaderTest.Models;

namespace UploaderTest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<BuildContext>(new CreateDatabaseIfNotExists<BuildContext>());

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
