using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using Api.Controllers;
using Data;
using Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace API.Tests.TestingContexts
{
    public class UserControllerContext
    {
        protected readonly UsersController controller;
        protected readonly Mock<IDRSRepository> repositoryMock = new Mock<IDRSRepository>();

        public UserControllerContext()
        {
            controller = new UsersController(repositoryMock.Object);

            User userMock = new User()
            {
                UserName = "testuser",
                Password = "testpass",
                FirstName = "Test",
                LastName = "Testsson",
                Id = 1,
                LastLoginDate = DateTime.Now,
                Email = "test@test.com",
                RegistrationDate = DateTime.Now,
                ReviewEntities = new List<ReviewEntity>(),
                Logs = new List<Log>()
            };

            repositoryMock.Setup(m => m.GetAllUsers()).Returns(GetUsers());
            repositoryMock.Setup(m => m.GetUser("Test1")).Returns(GetUser(1));
            repositoryMock.Setup(m => m.GetUser("Test2")).Returns(GetUser(2));
            repositoryMock.Setup(m => m.GetUser("Test3")).Returns(GetUser(3));


        }


        private static IQueryable<User> GetUsers()
        {
            IQueryable<User> fakeUsers = new List<User> {
                new User {Id=1, UserName = "Test1", Password="Test1pass", FirstName="Test1", LastName = "Testson1", Email = "test1@test1.com", DateOfBirth = null, LastLoginDate = DateTime.Now, RegistrationDate = DateTime.Now, Logs = new List<Log>(), ReviewEntities = new List<ReviewEntity>()},
                new User {Id=2, UserName = "Test2", Password="Test2pass", FirstName="Test2", LastName = "Testson2", Email = "test2@test2.com", DateOfBirth = null, LastLoginDate = DateTime.Now, RegistrationDate = DateTime.Now, Logs = new List<Log>(), ReviewEntities = new List<ReviewEntity>()},
                new User {Id=3, UserName = "Test3", Password="Test3pass", FirstName="Test3", LastName = "Testson3", Email = "test3@test3.com", DateOfBirth = null, LastLoginDate = DateTime.Now, RegistrationDate = DateTime.Now, Logs = new List<Log>(), ReviewEntities = new List<ReviewEntity>()},
           }.AsQueryable();
            return fakeUsers;
        }

        private static User GetUser(int Id)
        {
            return (from User u in GetUsers()
                    where u.Id == Id
                    select u).Single();
        }

        public void SetFakeHelper()
        {
             /*
             * fake helper 
             */
            var config = new HttpConfiguration();

            var request = new HttpRequestMessage(HttpMethod.Post, "http://testable.api.is.good/api/users/");

            var route = config.Routes.MapHttpRoute(
                name: "Users",
                routeTemplate: "api/users/{userName}",
                defaults: new { controller = "users", userName = RouteParameter.Optional }
                );

            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary
            {
                {"username", "test"}
            });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);

            var urlHelper = new UrlHelper(request);

            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;

            // inject a fake helper
            controller.Url = urlHelper;
        }
    }
}