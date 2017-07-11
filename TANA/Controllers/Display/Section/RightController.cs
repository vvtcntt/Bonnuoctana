using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TANA.Models;
namespace TANA.Controllers.Display.Section
{
    public class RightController : Controller
    {
        // GET: Right
        private TANAContext db=new TANAContext();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult PartialMenuProductAll()
        {
            var ListMenu = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == null).OrderBy(p => p.Ord).ToList();
            string chuoi = "";
            foreach (var item in ListMenu)
            {
                chuoi += "<li class=\"li_1\">";
                chuoi += "<a href=\"/" + item.Tag + ".html\" title=\"" + item.Name + "\"><i></i>" + item.Name + "</a>";
                int idcate = item.id;
                var ListChild = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == idcate).OrderBy(p => p.Ord).ToList();
                if (ListChild.Count > 0)
                {
                    chuoi += "<ul class=\"ul_2\">";
                    foreach (var item1 in ListChild)
                    {
                        chuoi += "<li class=\"li_2\"><a href=\"/" + item1.Tag + ".html\" title=\"" + item1.Name + "\">› " + item1.Name + "</a></li>";
                    }
                    chuoi += "</ul>";
                }
                chuoi += "<div class=\"Clear\"></div>";
                chuoi += "</li>";
            }
            ViewBag.chuoi = chuoi;
            return PartialView();
        }
        public PartialViewResult partialProductDetail(string tag)
        {
            var Product = db.tblProducts.First(p => p.Tag == tag);
            int idcates = int.Parse(Product.idCate.ToString());
            string check = db.tblGroupProducts.Find(idcates).ParentID.ToString();
            int idparent=0;
            var ListMenu = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == null).OrderBy(p => p.Ord).ToList();
            if (check != null && check != "")
            {
                idparent = int.Parse(check);
                ListMenu = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == idparent).OrderBy(p => p.Ord).ToList();
            }
                
         
           
            string chuoi = "";
            foreach (var item in ListMenu)
            {
                chuoi += "<li class=\"li_1\">";
                chuoi += "<a href=\"/" + item.Tag + ".html\" title=\"" + item.Name + "\"><i></i>" + item.Name + "</a>";
                int idcate = item.id;
                var ListChild = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == idcate).OrderBy(p => p.Ord).ToList();
                if (ListChild.Count > 0)
                {
                    chuoi += "<ul class=\"ul_2\">";
                    foreach (var item1 in ListChild)
                    {
                        chuoi += "<li class=\"li_2\"><a href=\"/" + item1.Tag + ".html\" title=\"" + item1.Name + "\">› " + item1.Name + "</a></li>";
                    }
                    chuoi += "</ul>";
                }
                chuoi += "<div class=\"Clear\"></div>";
                chuoi += "</li>";
            }
            ViewBag.chuoi = chuoi;
            return PartialView();
        }

        public PartialViewResult partialHanggia()
        {
            var listImage = db.tblImages.Where(p => p.Active == true && p.idCate == 3).OrderByDescending(p => p.Ord).ToList();
            string chuoi = "";
            foreach(var item in listImage)
            {
                chuoi += "<a href=\"" + item.Url + "\" title=\"" + item.Name + "\"><img src=\"" + item.Images + "\" alt=\"" + item.Name + "\" /></a>";
            }
            ViewBag.chuoi = chuoi;
            return PartialView();
        }
        public PartialViewResult partialNewsHomes()
        {
            var listnews = db.tblNews.Where(p => p.Active == true).OrderByDescending(p => p.DateCreate).Take(5).ToList();
            string chuoi = "";
            foreach(var item in listnews)
            {
                chuoi += "<div class=\"Tear_newshomes\">";
                chuoi += "<h4><a href=\"/Tin-tuc/" + item.Tag + "\" title=\"" + item.Name + "\">" + item.Name + "</a></h4>";
                chuoi += "<img src=\"" + item.Images + "\" alt=\"" + item.Name + "\" />";
                int leght = item.Description.Length;
                if(leght>100)
                {
                    chuoi += "<span>" + item.Description.Substring(0,100) + "...</span>";
                }
                else
                chuoi += "<span>" + item.Description + "</span>";
                chuoi += "</div>";
            }
            ViewBag.chuoi = chuoi;
            return PartialView();
        }
        public PartialViewResult partialAdw()
        {
            var listImage = db.tblImages.Where(p => p.Active == true && p.idCate == 6).OrderByDescending(p => p.Ord).ToList();
            string chuoi = "";
            foreach(var item in listImage)
            {
                chuoi += "<a href=\"" + item.Url + "\" title=\"" + item.Name + "\"><img src=\"" + item.Images + "\" alt=\"" + item.Name + "\" /></a>";
            }
            ViewBag.chuoi = chuoi;
            return PartialView();
        }
        public PartialViewResult partialProductList(string tag)
        {
            var Product = db.tblProducts.First(p => p.Tag == tag);
            int idcates = int.Parse(Product.idCate.ToString());
            string chuoi = "";
            var LitsProduct = db.tblProducts.Where(p => p.Active == true && p.idCate == idcates).OrderBy(p => p.Ord).Take(8).ToList();
            foreach (var item1 in LitsProduct)
            {
                chuoi += "<div class=\"Tear_1\">";
                chuoi += "<div class=\"OrderNow\">";
                chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item1.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                chuoi += "</div>";
                chuoi += "<div class=\"img\">";
                chuoi += "<a href=\"/san-pham/" + item1.Tag + "\" title=\"" + item1.Name + "\"><img src=\"" + item1.ImageLinkThumb + "\" alt=\"" + item1.Name + "\" /></a>";
                chuoi += "</div>";
                chuoi += "<a href=\"/san-pham/" + item1.Tag + "\" class=\"Name\" title=\"" + item1.Name + "\">" + item1.Name + "</a>";
                chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item1.Price) + "đ</span>";
                chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item1.PriceSale) + "đ</span>";
                chuoi += "</div>";
            }
            ViewBag.chuoi = chuoi;
            return PartialView();
        }
        public PartialViewResult partialMenunews()
        {

            return PartialView();
        }
 
    }
}