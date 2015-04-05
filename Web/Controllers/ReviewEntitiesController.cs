using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data;

namespace Api.Controllers
{
    public class ReviewEntitiesController : BaseApiController
    {
        public ReviewEntitiesController(IDRSRepository repository) : base(repository)
        {
        }


    }
}
