using System;
using Webshop_Models;
namespace Webshop_BusinessLayer.Repositories
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {

        void AddDevice(Device device);
        void UpdatePicture(Device d);
    }
}
