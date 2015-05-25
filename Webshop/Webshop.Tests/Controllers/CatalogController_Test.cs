using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Webshop.Controllers;
using Webshop.Models;
using Webshop_BusinessLayer.Repositories;
using Webshop_BusinessLayer.Services;
using Webshop_Models;

namespace Webshop.Tests.Controllers
{
    [TestClass]
    public class CatalogController_Test
    {/*
        private CataloogController controller = null;
        private IProductService productService = null;
        private IGenericRepository<Webshop.Models.OperatingSystem> repoOS = null;
        private IGenericRepository<ProgrammingFramework> repoFramework = null;
        private IBasketItemRepository<BasketItem> repoBasketItem = null;
        private IDeviceRepository repoDevice = null;

        public void Setup()
        {
            repoDevice = new DeviceRepository();
            repoFramework = new GenericRepository<ProgrammingFramework>();
            repoOS = new GenericRepository<Webshop.Models.OperatingSystem>();
            repoBasketItem = new BasketItemRepository<BasketItem>();
            productService = new ProductService(repoOS, repoFramework, repoDevice, repoBasketItem);
            controller = new CataloogController(productService);
        }
        [TestMethod]
        public void Index_Test()
        {
            ViewResult result = (ViewResult)controller.Index();
            List<Device> devices = result.Model as List<Device>;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<Device>));
            Assert.IsTrue(devices.Count == 5);
        }*/
    }
}
