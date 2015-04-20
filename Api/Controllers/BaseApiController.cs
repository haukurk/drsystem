using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Api.Models;
using Data;

namespace Api.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BaseApiController : ApiController
    {
        private IDRSRepository _repository;
        private ModelFactory _modelFactory;

        public BaseApiController(IDRSRepository repository)
        {
            _repository = repository;
        }

        public IDRSRepository DRSRepository
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
