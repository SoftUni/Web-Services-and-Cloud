namespace BugTracker.RestApi.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using BugTracker.Data;

    using EntityFramework.Extensions;

    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Owin;

    [TestClass]
    public class BugsControllerIntegrationTests
    {
        private TestServer httpTestServer;
        private HttpClient httpClient;

        [TestInitialize]
        public void TestInit()
        {
            // Start OWIN testing HTTP server with Web API support
            this.httpTestServer = TestServer.Create(appBuilder =>
            {
                var config = new HttpConfiguration();
                WebApiConfig.Register(config);
                appBuilder.UseWebApi(config);
            });
            this.httpClient = httpTestServer.HttpClient;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.httpTestServer.Dispose();
        }

        [TestMethod]
        public void ListBugs_EmptyDb_ShouldReturn200Ok_EmptyBugsList()
        {
            // Arrange
            CleanDatabase();

            // Act
            var httpResponse = httpClient.GetAsync("/api/bugs").Result;
            var bugs = httpResponse.Content.ReadAsAsync<List<Bug>>().Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.AreEqual(httpResponse.Content.Headers.ContentType.MediaType, "application/json");
            Assert.AreEqual(0, bugs.Count);
        }

        [TestMethod]
        public void ListBugs_NonEmptyDb_ShouldReturnBugsList()
        {
            // Arrange
            CleanDatabase();
            var dbContext = new BugTrackerDbContext();
            dbContext.Bugs.Add(
                new Bug()
                {
                    Text = "Bug #" + DateTime.Now.Ticks,
                    DateCreated = DateTime.Now,
                    Status = BugStatus.New
                });
            dbContext.Bugs.Add(
                new Bug()
                {
                    Text = "Bug #" + DateTime.Now.Ticks,
                    DateCreated = DateTime.Now,
                    Status = BugStatus.Fixed
                });
            dbContext.SaveChanges();

            // Act
            var httpResponse = httpClient.GetAsync("/api/bugs").Result;
            var bugsFromService = httpResponse.Content.ReadAsAsync<List<Bug>>().Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.AreEqual(httpResponse.Content.Headers.ContentType.MediaType, "application/json");

            var bugsFromDb = dbContext.Bugs.ToList();
            Assert.AreEqual(bugsFromDb.Count, bugsFromService.Count);

            // Assert the bugs in the DB are the same as the bugs returned from the service
            for (int i = 0; i < bugsFromService.Count; i++)
            {
                Assert.AreEqual(bugsFromService[i].Id, bugsFromDb[i].Id);
                Assert.AreEqual(bugsFromService[i].Text, bugsFromDb[i].Text);
                Assert.AreEqual(bugsFromService[i].Status, bugsFromDb[i].Status);
                Assert.AreEqual(bugsFromService[i].DateCreated.ToString(), bugsFromDb[i].DateCreated.ToString());
            }
        }

        [TestMethod]
        public void CreateBug_ShouldCreateBugInTheDb()
        {
            // Arrange
            CleanDatabase();
            var bugText = "Bug #" + DateTime.Now.Ticks;

            // Act
            var postContent = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("text", bugText)
            });
            var httpResponse = httpClient.PostAsync("/api/bugs", postContent).Result;
            var bugFromService = httpResponse.Content.ReadAsAsync<Bug>().Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.IsNotNull(httpResponse.Headers.Location);
            Assert.IsTrue(bugFromService.Id != 0);
            Assert.AreEqual(bugFromService.Text, bugText);
            Assert.AreEqual(bugFromService.Status, BugStatus.New);
            Assert.IsNotNull(bugFromService.DateCreated);

            // Assert the database values are correct
            var dbContext = new BugTrackerDbContext();
            var bugFromDb = dbContext.Bugs.FirstOrDefault();
            Assert.IsNotNull(bugFromDb);
            Assert.AreEqual(bugFromService.Id, bugFromDb.Id);
            Assert.AreEqual(bugFromService.Text, bugFromDb.Text);
            Assert.AreEqual(bugFromService.DateCreated.ToString(), bugFromDb.DateCreated.ToString());
            Assert.AreEqual(bugFromService.Status, bugFromDb.Status);
        }

        private void CleanDatabase()
        {
            // Clean all data in all database tables
            var dbContext = new BugTrackerDbContext();
            dbContext.Bugs.Delete();
            // ...
            // dbContext.AnotherEntity.Delete();
            // ...
            dbContext.SaveChanges();
        }
    }
}
