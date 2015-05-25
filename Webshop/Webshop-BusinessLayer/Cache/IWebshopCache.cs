using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Webshop_Models;

namespace Webshop_BusinessLayer.Cache
{
    public interface IWebshopCache
    {
        List<Device> GetDevicesFromCache();
        void RefreshCacheDevices();
    }
}
