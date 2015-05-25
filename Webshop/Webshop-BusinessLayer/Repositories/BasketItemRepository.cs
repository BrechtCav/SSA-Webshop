using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webshop_Models;
using System.Data.Entity;
using Webshop_BusinessLayer.Context;

namespace Webshop_BusinessLayer.Repositories
{
    public class BasketItemRepository : GenericRepository<BasketItem>, IBasketItemRepository
    {
        public IEnumerable<BasketItem> All(string username)
        {
            var query = (from bi in context.BasketItems.Include(d => d.Device) where bi.Username == username && bi.IsBought == false select bi);
            return query.ToList<BasketItem>();
        }
        public override BasketItem GetByID(object id)
		{
			var query = (from bi in context.BasketItems.Include(d => d.Device) where bi.ID == Convert.ToInt32(id) select bi);
			return query.SingleOrDefault<BasketItem>();
        }

        public override BasketItem Insert(BasketItem bi)
        {
            foreach (Webshop_Models.OperatingSystem os in bi.Device.OperatingSystems)
            {
                context.Entry<Webshop_Models.OperatingSystem>(os).State = EntityState.Unchanged;
            }

            foreach (ProgrammingFramework fw in bi.Device.Frameworks)
            {
                context.Entry<ProgrammingFramework>(fw).State = EntityState.Unchanged;
            }
            
            context.Entry<Device>(bi.Device).State = EntityState.Unchanged;
            BasketItem bm = context.BasketItems.Add(bi);
            context.SaveChanges();
            return bm;
        }
        public void DeleteByUser(string username)
        {
            var query = (from bi in context.BasketItems.Include(d => d.Device) where bi.Username == username && bi.IsBought == false select bi);
            List<BasketItem> listremove = query.ToList<BasketItem>();
            foreach(BasketItem item in listremove)
            {
                context.BasketItems.Attach(item);
                context.BasketItems.Remove(item);
            }
            context.SaveChanges(); 
        }
    }
}