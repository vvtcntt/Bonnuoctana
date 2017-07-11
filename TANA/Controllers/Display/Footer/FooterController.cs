using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TANA.Models;
namespace TANA.Controllers.Display.Footer
{
    public class FooterController : Controller
    {
        TANAContext db = new TANAContext();
        // GET: Footer
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ControlFooter()
        {
            var listPartner = db.tblImages.Where(p => p.Active == true && p.idCate == 2).OrderBy(p => p.Ord).ToList();
            string chuoipartner = "";
            foreach (var item in listPartner)
            {
                chuoipartner += "<a href=\"" + item.Url + "\" title=\"" + item.Name + "\"><img src=\"" + item.Name + "\" alt=\"" + item.Name + "\" /></a>";
            }
            ViewBag.chuoipartner = chuoipartner;
            var listHotline = db.tblHotlines.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            string chuoihotline = "";
            foreach (var item in listHotline)
            {
                chuoihotline += "<div class=\"Agency_Tear\">";
                chuoihotline += "<span class=\"ts\">" + item.Name + "</span>";
                chuoihotline += "<span class=\"dc\">Địa chỉ:" + item.Address + "</span>";
                chuoihotline += "<span class=\"dt\">Điện thoại :" + item.Mobile + " </span>";
                chuoihotline += "<span class=\"dt\">Hotline: <span>" + item.Hotline + "</span></span>";
                chuoihotline += " </div>";
            }
            ViewBag.chuoihotline = chuoihotline;
            var Listchinhsach = db.tblNews.Where(p => p.Active == true && p.idCate == 12).OrderBy(p => p.Ord).ToList();
            string chuoichinhsach = "";
            foreach (var item in Listchinhsach)
            {
                chuoichinhsach += "<li><a href=\"/Tin-tuc/" + item.Tag + "\" title=\"" + item.Name + "\">" + item.Name + "</a></li>";
            }
            ViewBag.chinhsach = chuoichinhsach;
            var ListBaogia = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == null).OrderBy(p => p.Ord).Take(5).ToList();
            string chuoibaogia = "";
            foreach (var item in ListBaogia)
            {
                chuoibaogia += "<a href=\"/Bao-gia/" + item.Tag + "\" title=\"" + item.Name + "\">" + item.Name + "</a>";
            }
            ViewBag.chuoibaogia = chuoibaogia;
            var ListGroup = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID==null).OrderBy(p=>p.Ord).ToList();
            string chuoi = "";
            string menu = "";
            foreach (var item in ListGroup)
            {
                chuoi += "<a href=\"/" + item.Tag + ".html\" title=\"" + item.Name + "\">" + item.Name + "</a>";
                menu += "<li><a href=\"/" + item.Tag + ".html\" title=\"" + item.Name + "\">" + item.Name + "</a>";
                int idcate = item.id;
                var listchid = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == idcate).OrderBy(p => p.Ord).ToList();
                if(listchid.Count>0)
                {
                    menu += " <ul>";
                    foreach(var item1 in listchid)
                    {
                        menu += "<li><a href=\"/" + item1.Tag + ".html\" title=\"" + item1.Name + "\">" + item1.Name + "</a></li>";
                    }
                   

                    menu += "</ul>";
                }
                   
                menu+="</li>";
            }
            ViewBag.menu = menu;
            ViewBag.chuoi = chuoi;
            var listUrl = db.tblUrls.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            string Url = "";
            foreach(var item in listUrl)
            {
                Url+="<a href=\""+item.Url+"\" title=\""+item.Name+"\">"+item.Name+"</a>";
            }
            ViewBag.Url = Url;
            return PartialView(db.tblConfigs.First());
        }
        public ActionResult Command(FormCollection collection)
        {


            string Email = collection["txtRes"];
            var listregister = db.tblRegisters.Where(p => p.Email == Email).ToList();
            if (listregister.Count > 0)
            { Session["Register"] = "<script>$(document).ready(function(){ alert('Đăng ký không thành công, Email của bạn đã được đăng ký từ trước, nếu bạn không nhận được thông tin khuyến mại vui lòng liên hệ qua hotline !') });</script>"; }
            else
            {
                tblRegister tblregister = new tblRegister();
             
                tblregister.Email = Email;
                db.tblRegisters.Add(tblregister);
                db.SaveChanges();
                Session["Register"] = "<script>$(document).ready(function(){ alert('Ban đã đặt đăng ký thành công') });</script>";
            }

            return Redirect("/");
        }
    }
}