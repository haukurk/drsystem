using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Api.Controllers;
using API.Tests.Helpers;
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
        }

        /*
        [TestMethod]
        public void TestLoginUser()
        {

        }

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
