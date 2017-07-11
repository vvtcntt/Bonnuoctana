using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TANA.Models;
namespace TANA.Controllers.Display.Header
{
    
    public class HeaderController : Controller
    {
        TANAContext db = new TANAContext();
        // GET: Header
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult HeaderControl()
        {
            return PartialView(db.tblConfigs.First());
        }
        public PartialViewResult MenuPartial()
        {
            var ListMenu = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == null).OrderBy(p => p.Ord).ToList();
            string chuoi = "";
            foreach (var item in ListMenu)
            {
                chuoi += "<li class=\"li1\">";
                chuoi += " <a href=\"/" + item.Tag + ".html\" title=\"" + item.Name + "\">" + item.Name + "</a>";
                int idcate = item.id;
                var Listchild = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == idcate).OrderBy(p => p.Ord).ToList();
                if (Listchild.Count > 0)
                {
                    chuoi += "<ul class=\"ul2\">";
                    foreach (var item1 in Listchild)
                    {
                        chuoi += "<li class=\"li2\"><a href=\"/" + item1.Tag + ".html\" title=\"" + item1.Name + "\">" + item1.Name + "</a>";
                        chuoi += "</li> ";
                    }
                    chuoi += " </ul>";
                }
                chuoi += "</li>";
            }
            ViewBag.chuoi = chuoi;
            return PartialView();
        }
        public ActionResult CommandSearch(FormCollection collection)
        {
            Session["Search"] = collection["txtSearch"];

            return Redirect("/Product/Search");
        }
    }
}