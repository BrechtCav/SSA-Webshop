using System;
using System.Collections.Generic;
using System.Web;
using Webshop_Models;
namespace Webshop_BusinessLayer.Services
{
    public interface IProductService
    {
        void AddDevice(Device nd);
        List<Device> GetAllDevices();
        List<ProgrammingFramework> GetAllFrameworks();
        List<Webshop_Models.OperatingSystem> GetAllOperatingSystems();
        Device GetDeviceById(int id);
        ProgrammingFramework GetFrameworkById(int id);
        Webshop_Models.OperatingSystem GetOSById(int Id);
        void UpdatePicture(int deviceId, HttpPostedFileBase picture);
        BasketItem InsertBasketItem(BasketItem ba);
        List<BasketItem> GetAllBasketItems(string username);
        BasketItem GetBasketItemById(int id);
        void Deletebasketitems(string username);
        OrderUser GetOrderUserByUsername(string id);
        Order AddOrder(Order o);
        Order AddOrderToDatabase(Order order);
        
    }
}
