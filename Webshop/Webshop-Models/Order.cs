using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop_Models
{
    public class Order
    {
        public int ID { get; set; }
        public OrderUser User { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public double Total { get; set; }
    }
}
