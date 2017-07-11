using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TANA.Models;
using PagedList;
using PagedList.Mvc;
namespace TANA.Controllers.Display.Section.Agency
{
    public class AgencyController : Controller
    {
        private TANAContext db = new TANAContext();
        // GET: Agency
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListAgency(int? page)
        {

            var listnews = db.tblAgencies.Where(p => p.Active == true).OrderByDescending(p => p.Ord).ToList();

            const int pageSize = 10;
            var pageNumber = (page ?? 1);
            // Thiết lập phân trang
            var ship = new PagedListRenderOptions
            {
                DisplayLinkToFirstPage = PagedListDisplayMode.Always,
                DisplayLinkToLastPage = PagedListDisplayMode.Always,
                DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
                DisplayLinkToNextPage = PagedListDisplayMode.Always,
                DisplayLinkToIndividualPages = true,
                DisplayPageCountAndCurrentLocation = false,
                MaximumPageNumbersToDisplay = 5,
                DisplayEllipsesWhenNotShowingAllPageNumbers = true,
                EllipsesFormat = "&#8230;",
                LinkToFirstPageFormat = "Trang đầu",
                LinkToPreviousPageFormat = "«",
                LinkToIndividualPageFormat = "{0}",
                LinkToNextPageFormat = "»",
                LinkToLastPageFormat = "Trang cuối",
                PageCountAndCurrentLocationFormat = "Page {0} of {1}.",
                ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.",
                FunctionToDisplayEachPageNumber = null,
                ClassToApplyToFirstListItemInPager = null,
                ClassToApplyToLastListItemInPager = null,
                ContainerDivClasses = new[] { "pagination-container" },
                UlElementClasses = new[] { "pagination" },
                LiElementClasses = Enumerable.Empty<string>()
            };
            ViewBag.ship = ship;

            ViewBag.Name = "Danh sách nhà phân phối bồn nước TÂN Á chính hãng trên toàn quốc";
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i> Nhà phân phối bồn nước";
            ViewBag.Title = "<title>Danh sách nhà phân phối bồn nước chính hãng trên toàn quốc</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"Danh sách nhà phân phối bồn nước chính hãng trên toàn quốc . ĐỊa chỉ cung cấp bồn nước Tân Á giá rẻ nhất.\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"bồn nước tân á\" /> ";
            return View(listnews.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult TagAgency(string tag, int? page)
        {
            string[] Mang1 = StringClass.COnvertToUnSign1(tag).Split('-');
            string chuoitag = "";
            for (int i = 0; i < Mang1.Length; i++)
            {
                if (i == 0)
                    chuoitag += Mang1[i];
                else
                    chuoitag += " " + Mang1[i];
            }
            int dem = 1;
            string name = "";
            List<tblAgency> ListNew = (from c in db.tblAgencies where c.Active == true select c).ToList();
            List<tblAgency> listnews = ListNew.FindAll(delegate(tblAgency math)
            {
                if (StringClass.COnvertToUnSign1(math.Tabs.ToUpper()).Contains(chuoitag.ToUpper()))
                {

                    string[] Manghienthi = math.Tabs.Split(',');
                    foreach (var item in Manghienthi)
                    {
                        if (dem == 1)
                        {
                            var kiemtra = StringClass.COnvertToUnSign1(item.ToUpper()).Contains(chuoitag.ToUpper());
                            if (kiemtra == true)
                            {
                                name = item;
                                dem = 0;
                            }
                        }
                    }

                    return true;
                }

                else
                    return false;
            }
            );
            const int pageSize = 10;
            var pageNumber = (page ?? 1);
            var ship = new PagedListRenderOptions
            {
                DisplayLinkToFirstPage = PagedListDisplayMode.Always,
                DisplayLinkToLastPage = PagedListDisplayMode.Always,
                DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
                DisplayLinkToNextPage = PagedListDisplayMode.Always,
                DisplayLinkToIndividualPages = true,
                DisplayPageCountAndCurrentLocation = false,
                MaximumPageNumbersToDisplay = 5,
                DisplayEllipsesWhenNotShowingAllPageNumbers = true,
                EllipsesFormat = "&#8230;",
                LinkToFirstPageFormat = "Trang đầu",
                LinkToPreviousPageFormat = "«",
                LinkToIndividualPageFormat = "{0}",
                LinkToNextPageFormat = "»",
                LinkToLastPageFormat = "Trang cuối",
                PageCountAndCurrentLocationFormat = "Page {0} of {1}.",
                ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.",
                FunctionToDisplayEachPageNumber = null,
                ClassToApplyToFirstListItemInPager = null,
                ClassToApplyToLastListItemInPager = null,
                ContainerDivClasses = new[] { "pagination-container" },
                UlElementClasses = new[] { "pagination" },
                LiElementClasses = Enumerable.Empty<string>()
            };
            ViewBag.ship = ship;

            ViewBag.Name = name;
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i> " + name + "";
            ViewBag.Title = "<title>" + name + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + name + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + name + "\" /> ";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Bonnuoctana.net/TagAgency/" + StringClass.NameToTag(chuoitag) + "\" />"; ;
            return View(listnews.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult AngencyDetail(string tag)
        {
            var tblagency = db.tblAgencies.First(p => p.Tag == tag);
            int idUser = int.Parse(tblagency.idUser.ToString());
            ViewBag.Username = db.tblUsers.Find(idUser).UserName;
            ViewBag.NameMenu = "Hệ thống phân phối";
            ViewBag.Title = "<title>" + tblagency.Name + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tblagency.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblagency.Tabs + "\" /> ";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tblagency.Name + "\" />";
            ViewBag.dcDescription = "<meta name=\"DC.description\" content=\"" + tblagency.Description + "\" />";
            string meta = "";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Bonnuoctana.net/Nha-phan-phoi/" + StringClass.NameToTag(tag) + "\" />";

            meta += "<meta itemprop=\"name\" content=\"" + tblagency.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + tblagency.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://Bonnuoctana.net" + tblagency.Images + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + tblagency.Name + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://Bonnuoctana.net" + tblagency.Images + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Bonnuoctana.net\" />";
            meta += "<meta property=\"og:description\" content=\"" + tblagency.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            int id = int.Parse(tblagency.id.ToString());
            if (tblagency.Tabs != null)
            {
                string Chuoi = tblagency.Tabs;
                string[] Mang = Chuoi.Split(',');

                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {

                    string tabs = Mang[i].ToString();
                    var listnew = db.tblAgencies.Where(p => p.Tabs.Contains(tabs) && p.id != id && p.Active == true).ToList();
                    for (int j = 0; j < listnew.Count; j++)
                    {
                        araylist.Add(listnew[j].id);
                    }

                }


                var Lienquan = db.tblAgencies.Where(p => araylist.Contains(p.id) && p.Active == true && p.id != id).OrderByDescending(p => p.Ord).Take(3).ToList();
                string chuoinew = "";
                if (Lienquan.Count > 0)
                {

                    chuoinew += " <div class=\"Lienquan\">";
                    for (int i = 0; i > Lienquan.Count; i++)
                    {
                        chuoinew += "<a href=\"/Nha-phan-phoi/" + Lienquan[i].Tag + "\" title=\"" + Lienquan[i].Name + "\"> " + Lienquan[i].Name + "</a>";
                    }
                    chuoinew += "</div>";
                }
                ViewBag.chuoinew = chuoinew;


                //Load tin mới

            }

            string chuoinewnew = "";
            var NewsNew = db.tblAgencies.Where(p => p.Active == true && p.id != id).OrderByDescending(p => p.DateCreate).Take(5).ToList();
            for (int i = 0; i < NewsNew.Count; i++)
            {
                chuoinewnew += "<li><a href=\"/Nha-phan-phoi/" + NewsNew[i].Tag + "\" title=\"" + NewsNew[i].Name + "\" rel=\"nofollow\"> " + NewsNew[i].Name + " <span>" + NewsNew[i].DateCreate + "</span></a></li>";
            }
            ViewBag.chuoinewnews = chuoinewnew;

            //load tag
            string chuoitag = "";
            if (tblagency.Tabs != null)
            {
                string Chuoi = tblagency.Tabs;
                string[] Mang = Chuoi.Split(',');

                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {

                    chuoitag += "<h2><a href=\"/TagAgency/" + StringClass.NameToTag(Mang[i]) + "\" title=\"" + Mang[i] + "\">" + Mang[i] + "</a></h2>";
                }
            }
            ViewBag.chuoitag = chuoitag;

            //Load root

            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i>" + tblagency.Name + "";
            return View(tblagency);
            return View();
        }
    }
}