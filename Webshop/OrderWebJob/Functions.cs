using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Webshop_BusinessLayer.Repositories;
using Newtonsoft.Json;
using Webshop_BusinessLayer.Services;
using Webshop_Models;

namespace OrderWebJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("orders")] string message, TextWriter log)
        {
            IOrderRepository repoOrder = new OrderRepository();
            IOrderLineRepository repoOrderLine = new OrderLineRepository();
            IOrderUserRepository repoOrderUser = new OrderUserRepository();
            IProductService ps = new ProductService(null, null, null, null, repoOrder, repoOrderLine, repoOrderUser);

            Order order = JsonConvert.DeserializeObject<Order>(message);

            try
            {
                ps.AddOrderToDatabase(order);
            }

            catch (Exception ex)
            {
                log.WriteLine(ex.Message);
            }
            log.WriteLine(message);
        }
    }
}
