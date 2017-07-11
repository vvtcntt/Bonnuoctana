using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TANA.Models;
namespace Bonnuoc.Controllers.Display.Section.Maps
{
    public class MapsController : Controller
    {
        // GET: Maps
        TANAContext db = new TANAContext();
        public ActionResult Index()
        {
            var tblMaps = db.tblMaps.First();

            ViewBag.Title = "<title>" + tblMaps.Name + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tblMaps.Description+ "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblMaps.Name+ "\" /> ";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tblMaps.Name + "\" />";
            ViewBag.dcDescription = "<meta name=\"DC.description\" content=\"" + tblMaps.Description + "\" />";
            string meta = "";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Bonnuoctana.net/Ban-do\" />";

            meta += "<meta itemprop=\"name\" content=\"" + tblMaps.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";

            ViewBag.Descriptionss = tblMaps.Description;
            ViewBag.Meta = meta;



            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i> Bản đồ";
            return View(tblMaps);
        }
    }
}