﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UrunKatalog.MvcWebUI.Entity;
using UrunKatalog.MvcWebUI.Identity;

namespace UrunKatalog.MvcWebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

			Database.SetInitializer(new DataInitializer());
			Database.SetInitializer(new IdentityInitializer());
		}
    }
}
