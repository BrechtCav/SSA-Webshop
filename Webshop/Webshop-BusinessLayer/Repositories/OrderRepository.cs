using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Webshop_Models;

namespace Webshop_BusinessLayer.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public override Order Insert(Order entity)
        {
            foreach(OrderLine line in entity.OrderLines)
            {
                context.Entry<BasketItem>(line.BasketItem).State = EntityState.Unchanged;
                context.OrderLines.Add(line);
            }
            OrderUser orderUser = (from ou in context.OrderUsers where ou.Username == entity.User.Username select ou).FirstOrDefault<OrderUser>();
            if(orderUser != null)
            {
                if(orderUser == entity.User)
                {
                    context.Entry<OrderUser>(entity.User).State = EntityState.Unchanged;
                }
                else
                {
                    context.Entry<OrderUser>(entity.User).State = EntityState.Modified;
                }
            }
            else
            {
                context.OrderUsers.Add(entity.User);
            }
            context.Orders.Add(entity);
            try
            {
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return entity;

        }
    }
}