using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TANA
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Product", "{tag}.html", new { controller = "Product", action = "ListProduct", tag = UrlParameter.Optional }, new { controller = "^p.*" }, new[] { "MyNamespace3" });
            routes.MapRoute("Chi_Tiet_San_Pham", "san-pham/{tag}", new { controller = "Product", action = "ProductDetail", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^ProductDetail$" });
            routes.MapRoute("Redrect", "Default.aspx/{tag}", new { controller = "Error", action = "Redriect", tag = UrlParameter.Optional }, new { controller = "^E.*", action = "^Redriect$" });
            
            routes.MapRoute("Capacity1", "{tag}-dt/{hang}", new { controller = "Product", action = "ListCapacity", tag = UrlParameter.Optional, hang = UrlParameter.Optional }, new { controller = "^P.*" }, new[] { "MyNamespace2" });
            routes.MapRoute("Capacity", "{tag}-dt", new { controller = "Product", action = "ListCapacity", tag = UrlParameter.Optional }, new { controller = "^P.*" }, new[] { "MyNamespace4" });
            routes.MapRoute("Baogia", "Bao-gia/{Tag}/{*catchall}", new { controller = "Baogia", action = "BaogiaDetail", tag = UrlParameter.Optional }, new { controller = "^B.*", action = "^BaogiaDetail$" });
            routes.MapRoute("Nha-phan-phoi", "Nha-phan-phoi/{Tag}/{*catchall}", new { controller = "Agency", action = "AngencyDetail", tag = UrlParameter.Optional }, new { controller = "^A.*", action = "^AngencyDetail$" });
            routes.MapRoute("ListNews", "0/{Tag}", new { controller = "News", action = "ListNews", tag = UrlParameter.Optional }, new { controller = "^N.*", action = "^ListNews$" });
            routes.MapRoute("NewsDetail", "tin-tuc/{Tag}/{*catchall}", new { controller = "News", action = "NewsDetail", tag = UrlParameter.Optional }, new { controller = "^N.*", action = "^NewsDetail$" });
            routes.MapRoute("TagNews", "TagNews/{Tag}/{*catchall}", new { controller = "News", action = "TagNews", tag = UrlParameter.Optional }, new { controller = "^N.*", action = "^TagNews$" });
            routes.MapRoute("TagNewss", "Tags/{Tag}/{*catchall}", new { controller = "News", action = "TagNews", tag = UrlParameter.Optional }, new { controller = "^N.*", action = "^TagNews$" });
            routes.MapRoute("TagAgency", "TagAgency/{Tag}/{*catchall}", new { controller = "Agency", action = "TagAgency", tag = UrlParameter.Optional }, new { controller = "^A.*", action = "^TagAgency$" });
            routes.MapRoute("TagProduct", "Tag/{Tag}/{*catchall}", new { controller = "Product", action = "Tag", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^Tag$" });
            routes.MapRoute("Tagcapacity", "TagCap/{Tag}/{*catchall}", new { controller = "Product", action = "TagCapacity", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^TagCapacity$" });
            routes.MapRoute("SearchProduct", "Search/{Tag}/{*catchall}", new { controller = "Product", action = "Search", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^Search$" });
            routes.MapRoute(name: "He-thong-phan-phoi", url: "He-thong-phan-phoi", defaults: new { controller = "Agency", action = "ListAgency" });
            routes.MapRoute(name: "Gioi-thieu", url: "Gioi-thieu", defaults: new { controller = "Introduction", action = "Index" });
            routes.MapRoute(name: "Lien-he", url: "Lien-he", defaults: new { controller = "Contact", action = "Index" });
            routes.MapRoute(name: "Ban-do", url: "Ban-do", defaults: new { controller = "Maps", action = "Index" });
            routes.MapRoute(name: "Admin", url: "Admin", defaults: new { controller = "Login", action = "LoginIndex" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
