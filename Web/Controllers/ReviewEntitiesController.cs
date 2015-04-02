using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DRS2Data;

namespace DRS2Web.Controllers
{
    public class ReviewEntitiesController : BaseApiController
    {
        public ReviewEntitiesController(IDRSRepository repository) : base(repository)
        {
        }


    }
}
