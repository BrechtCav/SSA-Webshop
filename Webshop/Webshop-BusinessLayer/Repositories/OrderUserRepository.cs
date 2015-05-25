using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webshop_Models;

namespace Webshop_BusinessLayer.Repositories
{
    public class OrderUserRepository : GenericRepository<OrderUser>, IOrderUserRepository
    {
        public OrderUser GetByUserName(string username)
        {
            var query = (from ou in context.OrderUsers where ou.Username == username select ou);
            return query.SingleOrDefault<OrderUser>();
        }
    }
}