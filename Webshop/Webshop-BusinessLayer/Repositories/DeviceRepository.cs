using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Webshop_BusinessLayer.Context;
using Webshop_Models;
namespace Webshop_BusinessLayer.Repositories
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public override IEnumerable<Device> All()
        {
            var query = (from d in context.Devices.Include(f => f.Frameworks)
                                               .Include(os => os.OperatingSystems)
                         select d);
            List<Device> test = new List<Device>( query.ToList<Device>());
            return test;

        }
        public override Device GetByID(object id)
        {
            int ID = Convert.ToInt32(id);
            var query = (from d in context.Devices.Include(f => f.Frameworks)
                                                 .Include(os => os.OperatingSystems)
                         where d.ID == ID
                         select d);
            return query.Single<Device>();
        }
        public void AddDevice(Device device)
        {
            foreach (Webshop_Models.OperatingSystem os in device.OperatingSystems)
            {
                context.OperatingSystems.Add(os);
                context.Entry<Webshop_Models.OperatingSystem>(os).State = EntityState.Unchanged;
            }
            foreach (ProgrammingFramework fr in device.Frameworks)
            {
                context.Frameworks.Add(fr);
                context.Entry<ProgrammingFramework>(fr).State = EntityState.Unchanged;
            }
            context.Devices.Add(device);
            context.SaveChanges();
        }
        public void UpdatePicture(Device d)
        {
            Device od = context.Devices.Find(d.ID);
            od.Picture = d.Picture;
            context.SaveChanges();
        }
    }
}