using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop_Models
{
    public class Price
    {
        public static double CalculateBasketItemTotal(BasketItem item)
        {
            return item.Device.Price * item.Count;
        }
        public static double CalculateBasketTotal(IEnumerable<BasketItem> items)
        {
            double price = 0;
            foreach(BasketItem item in items)
            {
                price += CalculateBasketItemTotal(item);
            }
            return price;
        }
    }
}
