using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_BusinessLayer.Repositories;
using Webshop_Models;

namespace Webshop.Controllers
{
    public class HomeController : Controller
    {
        private IAvailableCultureRepository LanguageSer = null;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public PartialViewResult SelectLanguage()
        {
            if(HttpContext.Request.Cookies["language"] !=null)
            {
                HttpCookie cookie = HttpContext.Request.Cookies["language"];
                String id = cookie.Value;
                ViewBag.Selected = id;
            }
            List<AvailableCulture> availablecultures = this.LanguageSer.AllAvailableCultures().ToList<AvailableCulture>();
            return PartialView(availablecultures);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}