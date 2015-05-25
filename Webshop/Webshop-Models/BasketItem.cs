using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop_Models;

namespace Webshop_Models
{
    public class BasketItem
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public Device Device { get; set; }
        public DateTime Timestamp { get; set; }
        public int Count { get; set; }
        public bool IsBought { get; set; }
    }
}
