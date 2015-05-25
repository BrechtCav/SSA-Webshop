using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Webshop_Models;

namespace Webshop_BusinessLayer.Repositories
{
    public class OrderLineRepository : GenericRepository<OrderLine>, IOrderLineRepository
    {
        public override OrderLine Insert(OrderLine entity)
        {
            context.BasketItems.Add(entity.BasketItem);
            context.Entry<BasketItem>(entity.BasketItem).State = EntityState.Unchanged;
            OrderLine ol = context.OrderLines.Add(entity);
            context.SaveChanges();
            return ol;
        }
    }
}