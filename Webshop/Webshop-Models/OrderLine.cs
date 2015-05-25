using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop_Models
{
    public class OrderLine
    {
        public int ID { get; set; }
        public BasketItem BasketItem { get; set; }
        public double Price { get; set; }
        public string Username { get; set; }
    }
}
