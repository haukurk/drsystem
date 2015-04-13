using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Controllers;
using Data;
using Data.Models;
using Moq;
using Ninject;
using Ninject.MockingKernel;

namespace API.Tests.Contexts
{
    public class UserControllerContext
    {
        protected readonly MockingKernel kernel;
        protected readonly UsersController controller;
        protected readonly Mock<IDRSRepository> repositoryMock;

        public UserControllerContext()
        {
            kernel = new MockingKernel();
            kernel.Inject(controller);
            controller = new UsersController(controller.DRSRepository);

            repositoryMock = Mock.Get(controller.DRSRepository);

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

            repositoryMock.Setup(m => m.GetUser(1)).Returns(userMock);

        }

    }
}
