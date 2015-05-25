using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.Models;
using Webshop_BusinessLayer.Repositories;
using Webshop_BusinessLayer.Services;
using Webshop_Models;

namespace Webshop.Controllers
{
    public class CataloogController : Controller
    {
        //public IGenericRepository<ProgrammingFramework> frameworkRepository;
        //public IGenericRepository<Webshop.Models.OperatingSystem> OSRepository;
        // GET: Cataloog

        private IProductService ps;
        public CataloogController(IProductService ps)
        {
            this.ps = ps;
        }
        public ActionResult Index()
        {
            return View(ps.GetAllDevices());
        }
        public ActionResult Detail(int Id)
        {
            return View(ps.GetDeviceById(Id));
        }
        [HttpPost]
        public ActionResult AddToCart(int id, int count)
        {
            Device d = ps.GetDeviceById(id);
            BasketItem bi = new BasketItem();
            bi.Device = d;
            bi.Timestamp = DateTime.Now;
            bi.Username = HttpContext.User.Identity.Name;
            bi.Count = count;
            bi.IsBought = false;
            BasketItem bs = ps.InsertBasketItem(bi);
            return RedirectToAction("Index", "Cataloog");
        }
    }
}