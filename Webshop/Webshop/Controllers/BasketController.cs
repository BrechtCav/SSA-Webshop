using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_BusinessLayer.Services;
using Webshop_Models;

namespace Webshop.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        // GET: Basket
        private IProductService ps;
        public BasketController(IProductService ps)
        {
            this.ps = ps;
        }
        public ActionResult Index()
        {
            return View(ps.GetAllBasketItems(this.HttpContext.User.Identity.Name));
        }
        public ActionResult Remove()
        {
            ps.Deletebasketitems(this.HttpContext.User.Identity.Name);
            return View("Index");
        }
        public ActionResult CountBasketItems()
        {
            int count = 0;
            List<BasketItem> listitems = ps.GetAllBasketItems(this.HttpContext.User.Identity.Name);
            foreach(BasketItem item in listitems)
            {
                count += item.Count;
            }
            ViewBag.Count = count;
            return PartialView("_CountBasketItems");
        }
        [HttpGet]
        public ActionResult Checkout()
        {
            Order order = new Order();
            order.User = new OrderUser();

            OrderUser registeredUser = ps.GetOrderUserByUsername(this.HttpContext.User.Identity.Name);
            if (registeredUser != null)
                order.User = registeredUser;

            return View(order);
        }
        [HttpPost]
        public ActionResult Checkout(Order newOrder)
        {
            string username = this.HttpContext.User.Identity.Name;

            newOrder.OrderLines = new List<OrderLine>();
            IEnumerable<BasketItem> basketItems = ps.GetAllBasketItems(username);

            foreach(BasketItem basketItem in basketItems)
            {
                newOrder.OrderLines.Add(new OrderLine()
                    {
                        BasketItem = basketItem,
                        Price = Price.CalculateBasketItemTotal(basketItem),
                        Username = username
                    });
            }
            newOrder.Total = Price.CalculateBasketTotal(basketItems);
            newOrder.User.Username = username;
            ps.AddOrder(newOrder);
            return RedirectToAction("Index", "Cataloog");
        }
    }
}