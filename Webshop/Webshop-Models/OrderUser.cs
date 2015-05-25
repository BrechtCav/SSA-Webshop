using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop_Models
{
    public class OrderUser
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string NumberAddition { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
    }
}
