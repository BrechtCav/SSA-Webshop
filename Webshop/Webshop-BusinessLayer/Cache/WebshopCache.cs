using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webshop_BusinessLayer.Repositories;
using Webshop_Models;

namespace Webshop_BusinessLayer.Cache
{
    public class WebshopCache : IWebshopCache
    {
        private static ConnectionMultiplexer connection = null;
        private static IDatabase cache = null;

        private IDeviceRepository deviceRepo = null;
        private IGenericRepository<Webshop_Models.OperatingSystem> osRepo = null;
        private IGenericRepository<ProgrammingFramework> frameworkRepo = null;
        private IBasketItemRepository basketitemRepo = null;

        public WebshopCache(IDeviceRepository deviceRepo, IGenericRepository<Webshop_Models.OperatingSystem> osRepo, IGenericRepository<ProgrammingFramework> frameworkRepo, IBasketItemRepository basketItemRepo)
        {
            this.deviceRepo = deviceRepo;
            this.osRepo = osRepo;
            this.frameworkRepo = frameworkRepo;
            this.basketitemRepo = basketItemRepo;
        }


        public static void Setup()
        {
            // Redis Cache instellen 

            var config = new ConfigurationOptions();
            config.SyncTimeout = 5000;
            config.EndPoints.Add("iotwebshopssa.redis.cache.windows.net");
            config.Ssl = true;
            config.Password = "IoHzGuNk4x8Jps8YYWSIt0xBhBFs6kcrhJmnU2Prv7Y=";

            connection = ConnectionMultiplexer.Connect(config);
            cache = connection.GetDatabase();
        }
        public List<Device> GetDevicesFromCache()
        {
            List<Device> cachedDevices = cache.Get("Devices") as List<Device>;
            if (cachedDevices != null)
                return cachedDevices;
            RefreshCacheDevices();

            return cache.Get("Devices") as List<Device>;
        }
        public void RefreshCacheDevices()
        {
            IEnumerable<Device> devices = deviceRepo.All().ToList<Device>();
            cache.Set("Devices", devices);
        }

    }
}