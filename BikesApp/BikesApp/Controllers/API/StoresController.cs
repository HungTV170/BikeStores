using Autofac;
using bikeStoreDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BikesApp.Controllers.API
{
    public class StoresController : ApiController
    {
        private readonly IComponentContext _context;
        public StoresController(IComponentContext context)
        {
            _context = context;
        }

        public List<DataObjTransferProduct> getListBikeyear_2016()
        {
            var service = _context.Resolve<BikesDBAccessLayer>();
            return service.getAllBikeyear_2016();
        }
    }
}
