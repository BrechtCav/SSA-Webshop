using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webshop_Models;

namespace Webshop_BusinessLayer.Repositories
{
    public class AvailableCultureRepo : GenericRepository<AvailableCulture>, IAvailableCultureRepository
    {
        private IGenericRepository<AvailableCulture> Availableculturerepository = null;

        public AvailableCultureRepo(IGenericRepository<AvailableCulture> availablecultureRepo)
        {
            this.Availableculturerepository = availablecultureRepo;
        }
        public IEnumerable<AvailableCulture> AllAvailableCultures()
        {
            return this.Availableculturerepository.All();
        }

        public AvailableCulture AvailableCultureById(int id)
        {
            return this.Availableculturerepository.GetByID(id);
        }
    }
}