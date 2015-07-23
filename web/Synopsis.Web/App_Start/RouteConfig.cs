using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Synopsis.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Home",
				url: "",
				defaults: new { controller = "Home", action = "Index" }
			);

			routes.MapRoute(
				name: "Request",
				url: "request",
				defaults: new {controller = "Home", action = "Request"}
			);
		}
	}
}