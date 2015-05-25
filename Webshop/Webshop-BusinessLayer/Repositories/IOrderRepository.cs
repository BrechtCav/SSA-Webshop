using System;
using Webshop_Models;
namespace Webshop_BusinessLayer.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Order Insert(Order entity);
    }
}
