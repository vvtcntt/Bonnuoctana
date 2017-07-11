using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TANA.Models;
namespace TANA.Controllers.Display.Section.Product
{
    public class ProductController : Controller
    {
        private TANAContext db = new TANAContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        List<string> Mangphantu = new List<string>();
        public List<string> Arrayid(int idParent)
        {

            var ListMenu = db.tblGroupProducts.Where(p => p.ParentID == idParent).ToList();

            for (int i = 0; i < ListMenu.Count; i++)
            {
                Mangphantu.Add(ListMenu[i].id.ToString());
                int id = int.Parse(ListMenu[i].id.ToString());
                Arrayid(id);

            }

            return Mangphantu;
        }
        string nUrl = "";
        public string UrlProduct(int idCate)
        {
            var ListMenu = db.tblGroupProducts.Where(p => p.id == idCate).ToList();
            for (int i = 0; i < ListMenu.Count; i++)
            {
                nUrl = " <a href=\"/" + ListMenu[i].Tag + ".html\" title=\"" + ListMenu[i].Name + "\"> " + " " + ListMenu[i].Name + "</a> <i></i>" + nUrl;
                string ids = ListMenu[i].ParentID.ToString();
                if (ids != null && ids != "")
                {
                    int id = int.Parse(ListMenu[i].ParentID.ToString());
                    UrlProduct(id);
                }
            }
            return nUrl;
        }
        public PartialViewResult partialProductHomes()
        {
            string chuoi = "";
            var ListMenu = db.tblGroupProducts.Where(p => p.Active == true && p.Priority==true).OrderBy(p => p.Ord).ToList();
            foreach(var item in ListMenu)
            {
                chuoi += "<div class=\"cls_Product\">";
                chuoi += "<div class=\"nvar_ct\">";
                chuoi += "<h2><a href=\"/" + item.Tag + ".html\" title=\"" + item.Name + "\">" + item.Name + "</a></h2>";
                chuoi += "</div>";
                chuoi += "<div class=\"Content_clsProduct\">";
                int idcate = item.id;
                List<string> Mang = new List<string>();
                Mang = Arrayid(idcate);
                Mang.Add(idcate.ToString());
                var LitsProduct = db.tblProducts.Where(p => p.Active == true && Mang.Contains(p.idCate.ToString()) && p.ViewHomes == true).OrderBy(p => p.Ord).ToList();
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


                chuoi += "</div>";
                chuoi += "</div>";
                Mangphantu.Clear();
            }
            ViewBag.chuoi = chuoi;
            return PartialView();
        }
        public ActionResult ProductDetail(string tag)
        {
            var tblproduct = db.tblProducts.First(p => p.Tag == tag);
            ViewBag.Title = "<title>" + tblproduct.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tblproduct.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tblproduct.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblproduct.Keyword + "\" /> ";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Bonnuoctana.net/san-pham/" + StringClass.NameToTag(tag) + "\" />";
            string meta = "";
            tblproduct.Visit = tblproduct.Visit + 1;
            db.SaveChanges();
            meta += "<meta itemprop=\"name\" content=\"" + tblproduct.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + tblproduct.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://Bonnuoctana.net" + tblproduct.ImageLinkThumb + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + tblproduct.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://Bonnuoctana.net" + tblproduct.ImageLinkThumb + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Bonnuoctana.net\" />";
            meta += "<meta property=\"og:description\" content=\"" + tblproduct.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta; int idcate = int.Parse(tblproduct.idCate.ToString());
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i>" + UrlProduct(idcate);
            string chuoitag = "";
            if (tblproduct.Keyword != null)
            {
                string Chuoi = tblproduct.Keyword;
                string[] Mang = Chuoi.Split(',');
                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {
                    string tagsp = StringClass.NameToTag(Mang[i]);
                    chuoitag += "<h2><a href=\"/Tag/" + tagsp + "\" title=\"" + Mang[i] + "\">" + Mang[i] + "</a></h2>";
                }
            }
            ViewBag.chuoitag = chuoitag;
            int idcap = 0;
            if(tblproduct.Capacity.ToString()!=null && tblproduct.Capacity.ToString()!="")
            {
            idcap = int.Parse(tblproduct.Capacity.ToString());
                var tblcapacity = db.tblCapacities.Find(idcap);
                ViewBag.capa = tblcapacity.Capacity;
                ViewBag.cappacity = tblcapacity.Capacity;
                ViewBag.songuoisd = tblcapacity.Note;
            }
           
            //Load tính năng
            var ListGroupCri = db.tblGroupCriterias.Where(p => p.idCate == idcate).ToList();
            List<int> Mang1 = new List<int>();
            for (int i = 0; i < ListGroupCri.Count; i++)
            {
                Mang1.Add(int.Parse(ListGroupCri[i].idCri.ToString()));
            }
            int idp = int.Parse(tblproduct.id.ToString());
            var ListCri = db.tblCriterias.Where(p => Mang1.Contains(p.id) && p.Active == true).ToList();
            string chuoi = "";
            #region[Lọc thuộc tính]


            for (int i = 0; i < ListCri.Count; i++)
            {
                int idCre = int.Parse(ListCri[i].id.ToString());
                var ListCr = (from a in db.tblConnectCriterias
                              join b in db.tblInfoCriterias on a.idCre equals b.id
                              where a.idpd == idp && b.idCri == idCre && b.Active == true
                              select new
                              {
                                  b.Name,
                                  b.Url,
                                  b.Ord
                              }).OrderBy(p => p.Ord).ToList();
                if (ListCr.Count > 0)
                {
                    chuoi += "<tr>";
                    chuoi += "<td>" + ListCri[i].Name + "</td>";
                    chuoi += "<td>";
                    int dem = 0;
                    string num = "";
                    if (ListCr.Count > 1)
                        num = "⊹ ";
                    foreach (var item in ListCr)
                        if (item.Url != null && item.Url != "")
                        {
                            chuoi += "<a href=\"" + item.Url + "\" title=\"" + item.Name + "\">";
                            if (dem == 1)
                                chuoi += num + item.Name;
                            else
                                chuoi += num + item.Name;
                            dem = 1;
                            chuoi += "</a>";
                        }
                        else
                        {
                            if (dem == 1)
                                chuoi += num + item.Name + "</br> ";
                            else
                                chuoi += num + item.Name + "</br> "; ;
                            dem = 1;
                        }
                    chuoi += "</td>";
                    chuoi += " </tr>";
                }
            }
            #endregion
            ViewBag.video = db.tblGroupProducts.Find(idcate).Video;
            ViewBag.chuoi = chuoi;
            //load sản phẩm gần giá
            string chuoilq = "";

            int nPrice = int.Parse(tblproduct.PriceSale.ToString());
            var List_01 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idcap && p.Tag != tag).OrderBy(p => p.PriceSale).Take(6).ToList();
            for (int i = 0; i < List_01.Count; i++)
            {
                chuoilq += " <div class=\"Tear_cg\">";
                chuoilq += " <a href=\"/san-pham/" + List_01[i].Tag + "\" class=\"name_cg\" title=\"" + List_01[i].Name + "\">" + List_01[i].Name + "</a>";
                chuoilq += " <img src=\"" + List_01[i].ImageLinkThumb + "\" title=\"" + List_01[i].Name + "\" />";
                chuoilq += "<span class=\"Price\">Giá  : <span>" + string.Format("{0:#,#}", List_01[i].PriceSale) + "đ</span></span>";
                chuoilq += "<span class=\"PriceSale\">Giá thị trường: <span>" + string.Format("{0:#,#}", List_01[i].Price) + "đ</span></span>";
                chuoilq += "</div>";
            }
            ViewBag.chuoilq = chuoilq;
            var tblManu = (from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b).Take(1).ToList();
            if(tblManu.Count>0)
            {
                ViewBag.manu = tblManu[0].Name;
                 ViewBag.urlmanu = tblManu[0].Images;
            }
            
            if(tblproduct.Material.ToString()!=null && tblproduct.Material.ToString()!="")
            {
                int nMaterial = int.Parse(tblproduct.Material.ToString());
                int nDesign = int.Parse(tblproduct.Design.ToString());
                var kiemtra = db.tblProducts.Where(p => p.Active == true && p.idCate == idcate && p.Design == nDesign && p.Material == nMaterial && p.id != idp && p.Capacity == idcap).Take(1).ToList();
                if (kiemtra.Count > 0)
                    ViewBag.xemthem = "<div class=\"xemthemchitiet\">Bạn có thể xem thêm " + tblManu[0].Name + " cùng dung tích khác : <a href=\"/san-pham/" + kiemtra[0].Tag + "\" title=\"" + kiemtra[0].Name + "\">   " + kiemtra[0].Name + "</a></div>";

            }
           
            var ImageList = (from a in db.tblConnectImages join b in db.tblImages on a.idImg equals b.id where a.idCate == idcate select b).OrderByDescending(p => p.Ord).Take(1).ToList();
            if (ImageList.Count > 0)
            {
                ViewBag.chuoianh = " <div class=\"Tear_Img\">  <a href=\"" + ImageList[0].Url + "\" title=\"" + ImageList[0].Name + "\"><img src=\"" + ImageList[0].Images + "\" alt=\"" + ImageList[0].Name + "\" /></a></div>";
            }
            return View(tblproduct);
        }
        public ActionResult ListProduct(string tag)
        {
            var tblgroupproduct = db.tblGroupProducts.First(p => p.Tag == tag);
            int id = tblgroupproduct.id;
            ViewBag.Title = "<title>" + tblgroupproduct.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tblgroupproduct.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tblgroupproduct.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblgroupproduct.Keyword + "\" /> ";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://bonnuoctana.net/" + StringClass.NameToTag(tag) + ".html\" />";
            string meta = "";
            meta += "<meta itemprop=\"name\" content=\"" + tblgroupproduct.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + tblgroupproduct.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://bonnuoctana.net" + tblgroupproduct.Images + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + tblgroupproduct.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://bonnuoctana.net" + tblgroupproduct.Images + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Bonnuoctana.net\" />";
            meta += "<meta property=\"og:description\" content=\"" + tblgroupproduct.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i>" + UrlProduct(tblgroupproduct.id);

            ViewBag.Name = tblgroupproduct.Name;
            ViewBag.Des = tblgroupproduct.Content;
            string chuoi = "";
            var ListGroup = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == id).OrderBy(p => p.Ord).ToList();
            if(ListGroup.Count>0)
            {
                foreach (var item in ListGroup)
                {
                    chuoi += "<div class=\"cls_Product\">";
                    chuoi += "<div class=\"nvar_ct\">";
                    chuoi += "<h2><a href=\"/" + item.Tag + ".html\" title=\"" + item.Name + "\">" + item.Name + "</a></h2>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Content_clsProduct\">";
                    int idcate = item.id;
                    //List<string> Mang = new List<string>();
                    //Mang = Arrayid(idcate);
                    //Mang.Add(idcate.ToString());
                    var LitsProduct = db.tblProducts.Where(p => p.Active == true && p.idCate == idcate).OrderBy(p => p.Ord).ToList();
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


                    chuoi += "</div>";
                    chuoi += "</div>";
                }
            }
            else
            {
                
                chuoi += "<div class=\"cls_Product\">";
                chuoi += "<div class=\"nvar_ct\">";
                chuoi += "<h2><a href=\"/" + tblgroupproduct.Tag + ".html\" title=\"" + tblgroupproduct.Name + "\">" + tblgroupproduct.Name + "</a></h2>";
                chuoi += "</div>";
                chuoi += "<div class=\"Content_clsProduct\">";
                int idcate = tblgroupproduct.id;
                
                var LitsProduct = db.tblProducts.Where(p => p.Active == true && p.idCate==idcate).OrderBy(p => p.Ord).ToList();
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


                chuoi += "</div>";
                chuoi += "</div>";

            }
            ViewBag.chuoi = chuoi;
            return View();
        }
        public ActionResult Tag(string tag)
        {
            string[] Mang1 = StringClass.COnvertToUnSign1(tag.ToUpper()).Split('-');
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
            List<tblProduct> ListProducts = (from c in db.tblProducts select c).ToList();
            List<tblProduct> listProduct = ListProducts.FindAll(delegate(tblProduct math)
            {
                string kd = StringClass.COnvertToUnSign1(math.Keyword.ToUpper());
                if (StringClass.COnvertToUnSign1(math.Keyword.ToUpper()).Contains(chuoitag.ToUpper()))
                {

                    string[] Manghienthi = math.Keyword.Split(',');
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
            ViewBag.Name = name;
            ViewBag.Title = "<title>" + name + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + name + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + name + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + name + "\" /> ";
            string meta = "";
            meta += "<meta itemprop=\"name\" content=\"" + name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + tag + "\" />";
            meta += "<meta itemprop=\"image\" content=\"\" />";
            meta += "<meta property=\"og:title\" content=\"" + name + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Bonnuoctana.net\" />";
            meta += "<meta property=\"og:description\" content=\"" + name + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";



              string chuoi = "";
             var listProduct1 = listProduct.Where(p => p.Active == true).OrderBy(p => p.PriceSale).ToList();

                chuoi += "<div class=\"cls_Product\">";
            
                chuoi += "<div class=\"Content_clsProduct\">";

                foreach (var item1 in listProduct1)
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


                chuoi += "</div>";
                chuoi += "</div>";

          
            ViewBag.chuoi = chuoi;
            return View();
        }
        public ActionResult Search()
        {
            if (Session["Search"] != null && Session["Search"] != "")
            {
                string tag = Session["Search"].ToString();
                ViewBag.Title = "<title>" + tag + "</title>";
                ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tag + "\" />";
                ViewBag.Description = "<meta name=\"description\" content=\"Tìm kiếm " + tag + "\"/>";
                ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tag + "\" /> ";
                ViewBag.Name = tag;
                ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i>" + tag;
                ViewBag.Name = tag;
                string chuoi = "";
                var ListProduct = db.tblProducts.Where(p => p.Active == true && p.Name.Contains(tag)).OrderBy(p => p.PriceSale).ToList();

                chuoi += "<div class=\"cls_Product\">";

                chuoi += "<div class=\"Content_clsProduct\">";

                foreach (var item1 in ListProduct)
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


                chuoi += "</div>";
                chuoi += "</div>";


                ViewBag.chuoi = chuoi;

            }
            return View();
        }

    }
}