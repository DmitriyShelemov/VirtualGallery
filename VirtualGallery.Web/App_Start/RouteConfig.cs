using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VirtualGallery.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapRoute(
            //    name: "SiteMap",
            //    url: "sitemap.xml",
            //    defaults: new { controller = MVC.CustomXmlSiteMap.Name, action = MVC.CustomXmlSiteMap.ActionNames.Index, page = 0 }
            //);

            //routes.MapRoute(
            //    name: "SiteMap-Paged",
            //    url: "sitemap-{page}.xml",
            //    defaults: new { controller = MVC.CustomXmlSiteMap.Name, action = MVC.CustomXmlSiteMap.ActionNames.Index, page = 0 }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = MVC.Home.Name, action = MVC.Home.ActionNames.Index, id = UrlParameter.Optional }
            );
        }
    }
}