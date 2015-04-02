using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DRS2Data;
using DRS2Web.Models;

namespace DRS2Web.Controllers
{
    
    public class BaseApiController : ApiController
    {
        private IDRSRepository _repository;
        private ModelFactory _modelFactory;

        public BaseApiController(IDRSRepository repository)
        {
            _repository = repository;
        }

        protected IDRSRepository DRSRepository
        {
            get { return _repository; }
        }

        protected ModelFactory DRSModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(Request, DRSRepository);
                }
                return _modelFactory;
            }
        }

    }
}
