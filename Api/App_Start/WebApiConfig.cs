using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Api.Filters;
using Newtonsoft.Json.Serialization;

namespace Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //config.Filters.Add(new ForceHttpsAttribute());

            // Systems 
            config.Routes.MapHttpRoute(
                name: "Systems",
                routeTemplate: "api/systems/{id}",
                defaults: new {controller = "systems", id = RouteParameter.Optional}
                );

            // Users 
            config.Routes.MapHttpRoute(
                name: "Users",
                routeTemplate: "api/users/{userName}",
                defaults: new { controller = "users", userName = RouteParameter.Optional }
                );

            // Review Entities
            config.Routes.MapHttpRoute(
                name: "ReviewEntities",
                routeTemplate: "api/reviews/{id}",
                defaults: new { controller = "reviewentities", id = RouteParameter.Optional }
                );

            // Logs
            config.Routes.MapHttpRoute(
                name: "Logs",
                routeTemplate: "api/logs/{logId}",
                defaults: new { controller = "logs", logId = RouteParameter.Optional }
                );

            // Issues
            config.Routes.MapHttpRoute(
                name: "Issues",
                routeTemplate: "api/issues/{id}",
                defaults: new { controller = "issues", id = RouteParameter.Optional }
                );

            // Lets make our JSON format be in camelCase format for JS developers.
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }
    }
}
