using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Webshop_Models;
using Webshop_BusinessLayer.Repositories;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Queue;
using Webshop_BusinessLayer.Cache;

namespace Webshop_BusinessLayer.Services
{
    public class ProductService :IProductService
    {
        private IGenericRepository<Webshop_Models.OperatingSystem> repoOS = null;
        private IGenericRepository<ProgrammingFramework> repoFramework = null;
        private IDeviceRepository repoDevice = null;
        private IBasketItemRepository repoBasketItem = null;
        private IOrderRepository repoOrder = null;
        private IOrderLineRepository repoOrderLine = null;
        private IOrderUserRepository repoOrderUser = null;

        private IWebshopCache cacheWebShop = null;

        public ProductService() { }

        public ProductService
            (
                IGenericRepository<Webshop_Models.OperatingSystem> repoOS,
                IGenericRepository<ProgrammingFramework> repoFramework,
                IDeviceRepository repoDevice,
                IBasketItemRepository repoBasketItem,
                IOrderRepository repoOrder,
                IOrderLineRepository repoOrderLine,
                IOrderUserRepository repoOrderUser
            )
        {
            this.repoOS = repoOS;
            this.repoFramework = repoFramework;
            this.repoDevice = repoDevice;
            this.repoBasketItem = repoBasketItem;
            this.repoOrder = repoOrder;
            this.repoOrderLine = repoOrderLine;
            this.repoOrderUser = repoOrderUser;
        }
        public List<Device> GetAllDevices()
        {
            return repoDevice.All().ToList<Device>();
         //   return cacheWebShop.GetDevicesFromCache();
        }
        public Device GetDeviceById(int id)
        {
            return repoDevice.GetByID(id);
        }
        public void AddDevice(Device nd)
        {
            repoDevice.AddDevice(nd);
            cacheWebShop.RefreshCacheDevices();
            //repoDevice.AddDevice(nd);
        }
        public void UpdatePicture(int deviceId, HttpPostedFileBase picture)
        {
            string fileName = Path.GetFileName(picture.FileName);
            Device device = repoDevice.GetByID(deviceId);
            device.Picture = fileName;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("images");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            //blockBlob.UploadFromStream(picture.InputStream);
            repoDevice.UpdatePicture(device);
        }
        public List<Webshop_Models.OperatingSystem> GetAllOperatingSystems()
        {
            return repoOS.All().ToList<Webshop_Models.OperatingSystem>();
        }
        public Webshop_Models.OperatingSystem GetOSById(int Id)
        {
            return repoOS.GetByID(Id);
        }
        public List<ProgrammingFramework> GetAllFrameworks()
        {
            return repoFramework.All().ToList<ProgrammingFramework>();
        }
        public ProgrammingFramework GetFrameworkById(int id)
        {
            return repoFramework.GetByID(id);
        }
        public BasketItem GetBasketItemById(int id)
        {
            return repoBasketItem.GetByID(id);
        }
        public List<BasketItem> GetAllBasketItems(string username)
        {
            return repoBasketItem.All(username).ToList<BasketItem>();
        }
        public BasketItem InsertBasketItem(BasketItem bi)
        {
            return repoBasketItem.Insert(bi);
        }
        public void Deletebasketitems(string username)
        {
            repoBasketItem.DeleteByUser(username);
        }
        public OrderUser GetOrderUserByUsername(string username)
        {
            return repoOrderUser.GetByUserName(username);
        }
        public Order AddOrder(Order o)
        {
            foreach(OrderLine ol in o.OrderLines)
            {
                ol.BasketItem.IsBought = true;
                repoBasketItem.Update(ol.BasketItem);
                repoBasketItem.SaveChanges();
            }
            string json = JsonConvert.SerializeObject(o);
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("orders");
            queue.CreateIfNotExists();
            CloudQueueMessage message = new CloudQueueMessage(json);
            queue.AddMessage(message);
            return o;
        }
        public Order AddOrderToDatabase(Order order)
        {
            return repoOrder.Insert(order);
        }
    }
}