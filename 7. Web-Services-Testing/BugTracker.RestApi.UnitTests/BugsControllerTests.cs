namespace BugTracker.RestApi.Tests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Web.Http;
    using System.Web.Http.Routing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;

    using BugTracker.Data;
    using BugTracker.RestApi.Controllers;
    using BugTracker.Repositories;

    using Moq;

    [TestClass]
    public class BugsControllerTests
    {
        [TestMethod]
    public void GetAll_WhenValid_ShouldReturnBugsCollection()
    {
        // Arrange
        var repo = new RepositoryMock<Bug>();

        var bugs = new List<Bug>()
        {
            new Bug()
            {
                Text = "Test bug #1"
            },
            new Bug()
            {
                Text = "Test bug #2"
            },
            new Bug()
            {
                Text = "Test bug #3"
            }
        };            
            
        repo.Entities = bugs;

        var controller = new BugsController(repo);

        // Act
        var result = controller.GetAll();

        // Assert
        CollectionAssert.AreEquivalent(bugs, result.ToList<Bug>());
    }

        [TestMethod]
        public void PostBug_WhenBugTextIsValid_ShouldBeAddedToRepoWithDateCreatedAndStatusNew()
        {
            // Arrange
            var repo = new RepositoryMock<Bug>();
            repo.IsSaveCalled = false;
            repo.Entities = new List<Bug>();
            var newBug = new Bug()
            {
                Text = "Test bug"
            };
            var controller = new BugsController(repo);
            this.SetupController(controller, "bugs");

            // Act
            var httpResponse = controller.PostBug(newBug).ExecuteAsync(new CancellationToken()).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.IsNotNull(httpResponse.Headers.Location);

            var bugFromService = httpResponse.Content.ReadAsAsync<Bug>().Result;
            Assert.AreEqual(bugFromService.Text, newBug.Text);
            Assert.AreEqual(bugFromService.Status, BugStatus.New);
            Assert.IsNotNull(bugFromService.DateCreated);

            // Assert the repository values are correct
            Assert.AreEqual(repo.Entities.Count, 1);
            var bugInRepo = repo.Entities.First();
            Assert.AreEqual(newBug.Text, bugInRepo.Text);
            Assert.IsNotNull(bugInRepo.DateCreated);
            Assert.AreEqual(BugStatus.New, bugInRepo.Status);
            Assert.IsTrue(repo.IsSaveCalled);
        }

        [TestMethod]
        public void PostBug_WhenBugTextIsInvalid_ShouldReturnBadRequest()
        {
            // Arrange
            var repo = new RepositoryMock<Bug>();
            var controller = new BugsController(repo);
            this.SetupController(controller, "bugs");

            // Act
            var newBug = new Bug() { Text = null };
            var result = controller.PostBug(newBug).ExecuteAsync(new CancellationToken()).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void GetAll_WhenValid_ShouldReturnBugsCollection_WithMoq()
        {
            // Arrange
            var repoMock = new Mock<IRepository<Bug>>();

            Bug[] bugs =
            {
                new Bug() { Text = "Test bug #1" },
                new Bug() { Text = "Test bug #2" }
            };

            repoMock.Setup(repo => repo.All())
                .Returns(bugs.AsQueryable());

            var controller = new BugsController(repoMock.Object);
            
            // Act
            var result = controller.GetAll();

            // Assert
            CollectionAssert.AreEquivalent(bugs, result.ToArray<Bug>());
        }

        [TestMethod]
        public void PostBug_WhenValid_ShouldAddBugToDb_WithMoq()
        {
            // Arrange
            var repoMock = new Mock<IRepository<Bug>>();

            List<Bug> bugs = new List<Bug>()
            {
                new Bug() { Text = "Test bug #1" },
                new Bug() { Text = "Test bug #2" }
            };

            repoMock.Setup(repo => repo.Add(It.IsAny<Bug>()))
                .Callback((Bug b) => bugs.Add(b));

            var controller = new BugsController(repoMock.Object);
            this.SetupController(controller, "bugs");

            // Act
            var newBug = new Bug() { Text = "new bug" };
            var result = controller.PostBug(newBug).ExecuteAsync(new CancellationToken()).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Assert.AreEqual(newBug.Text, bugs.Last().Text);
            Assert.AreEqual(BugStatus.New, bugs.Last().Status);
            Assert.IsNotNull(bugs.Last().DateCreated);
        }

        private void SetupController(ApiController controller, string controllerName)
        {
            string serverUrl = "http://sample-url.com";

            // Setup the Request object of the controller
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(serverUrl)
            };
            controller.Request = request;

            // Setup the configuration of the controller
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            controller.Configuration = config;
            
            // Apply the routes to the controller
            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary
                {
                    { "controller", controllerName }
                });
        }
    }
}
