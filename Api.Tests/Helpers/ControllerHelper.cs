using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

namespace API.Tests.Helpers
{
    public static class ControllerHelper
    {
        public static void SetupControllerForTests(ApiController controller)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://testapi.com/api/");
            var route = config.Routes.MapHttpRoute("Users", "api/users/{userName}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary
            {
                {"id", Guid.Empty},
                {"controller", "organization"}
            });
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            UrlHelper urlHelper = new UrlHelper(request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            /// inject a fake helper
            controller.Url = urlHelper;
        }
    }
}