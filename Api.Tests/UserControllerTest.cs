using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Api.Controllers;
using Api.Models;

using API.Tests.TestingContexts;
using Data;
using Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace API.Tests
{

    [TestClass]
    public class UserControllerTest : UserControllerContext
    {

        [TestMethod]
        public void TestGetUsers()
        {
            // Arrange
            SetFakeHelper();

            // Act
            var users = controller.Get();
            
            // Assert
            Assert.IsNotNull(users, "Result is null");
            Assert.IsInstanceOfType(users, typeof(IEnumerable), "Wrong Model");
            Assert.AreEqual(3, users.Count(), "Got wrong number of users..");
        }

        [TestMethod]
        public void TestPermissionForResourceNotAllowed()
        {

            /*
             *   This test is currently failing due to auth test issue.
             */

            // Arrange 
            SetFakeHelper();

            // Act
            var userResponseMessage = controller.Get("test2");

            // Assert
            Assert.AreEqual(userResponseMessage.StatusCode, HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public void TestPermissionForResourceAllowed()
        {
            // Arrange 
            SetFakeHelper();
            // TODO: We are not testing the auth like this. FIX, add headers.
            var identity = new GenericIdentity("Test2");
            Thread.CurrentPrincipal = new GenericPrincipal(identity, null);

            // Act
            var userResponseMessage = controller.Get("Test2");

            // Assert
            Assert.AreEqual(userResponseMessage.StatusCode, HttpStatusCode.OK);

            UserBaseModel user = null;
            userResponseMessage.TryGetContentValue(out user);

            Assert.IsInstanceOfType(user, typeof(UserDetailModel), "Wrong Model");
        }

        
        [TestMethod]
        public void TestFindingANonExistingUser()
        {
            // Arrange 
            SetFakeHelper();
            // TODO: We are not testing the auth like this. FIX, add headers.
            var identity = new GenericIdentity("SomeUserThatDoesnotExists");
            Thread.CurrentPrincipal = new GenericPrincipal(identity, null);

            // Act
            var userResponseMessage = controller.Get("SomeUserThatDoesnotExists");

            // Assert
            Assert.AreEqual(userResponseMessage.StatusCode, HttpStatusCode.NotFound);
        }

        /*
        [TestMethod]
        public void TestUpdateUser()
        {

        }

        [TestMethod]
        public void TestUpdateUserUnAuthorized()
        {

        }

        [TestMethod]
        public void TestGetUserInfo()
        {

        }

        [TestMethod]
        public void TestGetUserInfoUnAuthorizer()
        {

        }
        */
    }
}
