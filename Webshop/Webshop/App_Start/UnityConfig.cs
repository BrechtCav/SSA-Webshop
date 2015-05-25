using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Webshop_BusinessLayer.Repositories;
using Webshop_Models;
using Webshop_BusinessLayer.Services;
using Webshop.Controllers;

namespace Webshop
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IGenericRepository<Webshop_Models.ProgrammingFramework>, GenericRepository<Webshop_Models.ProgrammingFramework>>();
            container.RegisterType<IGenericRepository<Webshop_Models.OperatingSystem>, GenericRepository<Webshop_Models.OperatingSystem>>();
            container.RegisterType<IDeviceRepository, DeviceRepository>();
            container.RegisterType<IBasketItemRepository, BasketItemRepository>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<IOrderLineRepository, OrderLineRepository>();
            container.RegisterType<IOrderUserRepository, OrderUserRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}