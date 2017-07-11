using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TANA.Models;
namespace TANA.Controllers.Display
{
    public class DefaultController : Controller
    {
        // GET: Default
        private TANAContext db = new TANAContext();
        public ActionResult Index()
        {
            tblConfig config = db.tblConfigs.First();
            ViewBag.Title = "<title>" + config.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + config.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + config.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + config.Keywords + "\" /> ";
            ViewBag.h1 = "<h1 class=\"h1\">" + config.Title + "</h1>";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Bonnuoctana.net\" />";
            string meta = "";
            meta += "<meta itemprop=\"name\" content=\"" + config.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + config.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://Bonnuoctana.net" + config.Logo + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + config.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://Bonnuoctana.net" + config.Logo + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Bonnuoctana.net\" />";
            meta += "<meta property=\"og:description\" content=\"" + config.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            if (Session["Register"] != null && Session["Register"] != "")
            {
                ViewBag.register = Session["Register"].ToString();
                Session["Register"] = "";
            }
            return View();
        }
        public PartialViewResult partialdefault()
        { 
            return PartialView(db.tblConfigs.First());
        }
        public PartialViewResult partialSlide()
        {
            var listimageslide = db.tblImages.Where(p => p.Active == true && p.idCate == 1).OrderByDescending(p => p.Ord).Take(4).ToList();
            string chuoislide = "";
            for (int i = 0; i < listimageslide.Count; i++)
            {
                if (i == 0)
                {
                    chuoislide += "url(" + listimageslide[i].Images + ") " + (1200 * i) + "px 0 no-repeat";
                }
                else
                {

                    chuoislide += ", url(" + listimageslide[i].Images + ") " + (1200 * i) + "px 0 no-repeat";
                }
            }
            ViewBag.chuoislide = chuoislide;
            return PartialView(listimageslide);
        }
    }
}