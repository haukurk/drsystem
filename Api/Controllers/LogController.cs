using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Controllers;
using Data;

namespace API.Controllers
{
    public class LogController : BaseApiController
    {
        public LogController(IDRSRepository repository) : base(repository)
        {
        }


    }
}
