using Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data;

namespace API.Controllers
{
    public class IssuesController : BaseApiController
    {
        public IssuesController(IDRSRepository repository) : base(repository)
        {
        }


    }
}
