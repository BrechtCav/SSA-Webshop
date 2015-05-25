using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop_Models
{
    [Serializable]
    public class Device
    {
        public int ID {get;set;}
        public string Name{get;set;}
        public double Price{get;set;}
        public double RentPrice{get;set;}
        public int Stock{get;set;}
        public string Picture { get; set; }
        public string Description { get; set; }
        public List<OperatingSystem> OperatingSystems{get;set;}
        public List<ProgrammingFramework> Frameworks {get;set;}

    }
}
