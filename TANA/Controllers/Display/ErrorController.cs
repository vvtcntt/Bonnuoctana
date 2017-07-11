using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TANA.Models;
namespace TANA.Controllers.Display
{
    public class ErrorController : Controller
    {
        TANAContext db = new TANAContext();
        // GET: Error
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Redriect()
        {
            string f = Request.QueryString["f"].ToString();
           switch (f)
           {
               case "Product_Detail":
                   {
                       int idProduct =int.Parse( Request.QueryString["idProduct"].ToString());
                       string tag = db.tblProducts.Find(idProduct).Tag;
                       return Redirect("/san-pham/"+tag);

                   }
               case "New_Detail":
                   {
                       int idNews = int.Parse(Request.QueryString["idNews"].ToString());
                       string tag = db.tblNews.Find(idNews).Tag;
                       return Redirect("/tin-tuc/" + tag);
                   }
               case "List_Product":
                   {
                       int idMenu = int.Parse(Request.QueryString["idMenu"].ToString());
                       string tag = db.tblGroupProducts.Find(idMenu).Tag;
                       return Redirect("/" + tag+".html");
                   }
               case "New_Catagories":
                   {
                       int idMenu = int.Parse(Request.QueryString["idMenu"].ToString());
                       string tag = db.tblGroupNews.Find(idMenu).Tag;
                       return Redirect("/0/" + tag);
                   }
               
           }
           string m = Request.QueryString["m"].ToString();
            switch(m)
            {
                case "NPP":
                    {
                        int idDMNPP = int.Parse(Request.QueryString["idDMNPP"].ToString());
                        string tag = db.tblAgencies.Find(idDMNPP).Tag;
                        return Redirect("/Nha-phan-phoi/" + tag);
                    }
            }
            return View();
        }
    }
}