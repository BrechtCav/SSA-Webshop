using System;
using Webshop_Models;
namespace Webshop_BusinessLayer.Repositories
{
     public interface IOrderUserRepository : IGenericRepository<OrderUser>
    {
        OrderUser GetByUserName(string username);
    }
}
