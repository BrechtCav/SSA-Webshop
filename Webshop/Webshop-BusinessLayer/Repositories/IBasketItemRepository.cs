using System;
using System.Collections.Generic;
using Webshop_Models;
namespace Webshop_BusinessLayer.Repositories
{
    public interface IBasketItemRepository : IGenericRepository<BasketItem>
    {
        IEnumerable<BasketItem> All(string username);
        BasketItem GetByID(object id);
        BasketItem Insert(BasketItem bi);
        void DeleteByUser(string username);
    }
}
