using System;
using Webshop_Models;
namespace Webshop_BusinessLayer.Repositories
{
    public interface IOrderLineRepository : IGenericRepository<OrderLine>
    {
        OrderLine Insert(OrderLine entity);
    }
}
