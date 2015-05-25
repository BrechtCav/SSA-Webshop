using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop_Models
{
    public class ProgrammingFramework
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Device> Devices { get; set; }
    }
}