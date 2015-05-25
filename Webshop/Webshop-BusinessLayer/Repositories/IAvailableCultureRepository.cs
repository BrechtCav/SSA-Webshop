using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop_Models;

namespace Webshop_BusinessLayer.Repositories
{
    public interface IAvailableCultureRepository : IGenericRepository<AvailableCulture>
    {
        IEnumerable<AvailableCulture> AllAvailableCultures();

        AvailableCulture AvailableCultureById(int id);
    }
}
