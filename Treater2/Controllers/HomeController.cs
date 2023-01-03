using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Treater2.DAL;
using Treater2.Models;
using Treater2.ViewModels;


namespace Treater2.Controllers
{
    public class HomeController : Controller
    {
        private TreatingContext db = new TreatingContext();
        //Get Home/Index
        public ActionResult Settings()
        {
            var homeIndex = new HomeIndex();
            if (Session["Treater"] != null)
            {
                homeIndex.TreatingLineID = (int)Session["Treater"];
            }
            else
            {
                //homeIndex.TreatingLineID = 3;
            }
            //homeIndex.TreatingLineID = TID ?? 3;
            ViewBag.TreatingLineID = new SelectList(db.TreatingLines, "TreatingLineID", "TreaterName", homeIndex.TreatingLineID);
            return View(homeIndex);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings([Bind(Include = "TreatingLineID")] HomeIndex homeIndex)
        {
            if (ModelState.IsValid)
            { 
                if (homeIndex.TreatingLineID != 0)
                {
                    Session["Treater"] = homeIndex.TreatingLineID;
                    return RedirectToAction("Index", homeIndex.TreatingLineID);
                }
            }
            return View(homeIndex);
        }

        public ActionResult Index(int? TID)
        {
            var homeIndex = new HomeIndex();
            if (TID == null)
            {
                if (Session["Treater"] != null)
                {
                    TID = (int)Session["Treater"];
                    ViewBag.Treater = db.TreatingLines.Find(TID);
                }
                else
                {
                    return RedirectToAction("Settings");
                }
            }
            ViewBag.Treater = db.TreatingLines.Find(TID);
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
    public static class SessionExtensions
    {
        public static T GetDataFromSession<T>(this HttpSessionStateBase session, string key)
        {
                var retrievedcookie = (T)session[key];
                return retrievedcookie;
        }

        public static void SetDataInSession<T>(this HttpSessionStateBase session, string key, object value)
        {
            session[key] = value;
        }
    }
}