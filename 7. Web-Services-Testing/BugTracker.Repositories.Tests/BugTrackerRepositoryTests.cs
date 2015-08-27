namespace BugTracker.Repositories.Tests
{
    using System.Data.Entity.Validation;
    using System.Transactions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BugTracker.Data;
    using BugTracker.Repositories;
    using System;

    [TestClass]
    public class BugTrackerRepositoryTests
    {
        private static TransactionScope tran;

        [TestInitialize]
        public void TestInit()
        {
            // Start a new temporary transaction
            tran = new TransactionScope();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            // Rollback the temporary transaction
            tran.Dispose();
        }

        [TestMethod]
        public void AddBug_WhenBugIsAddedToDb_ShouldReturnBug()
        {
            // Arrange -> prapare the objects
            var repo = new BugsRepository(new BugTrackerDbContext());
            var bug = new Bug()
            {
                Text = "Sample New Bug",
                DateCreated = DateTime.Now,
                Status = BugStatus.New
            };

            // Act -> perform some logic
            repo.Add(bug);
            repo.SaveChanges();

            // Assert -> validate the results
            var bugFromDb = repo.Find(bug.Id);

            Assert.IsNotNull(bugFromDb);
            Assert.AreEqual(bug.Text, bugFromDb.Text);
            Assert.AreEqual(bug.Status, bugFromDb.Status);
            Assert.AreEqual(bug.DateCreated, bugFromDb.DateCreated);
            Assert.IsTrue(bugFromDb.Id != 0);
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddBug_WhenBugIsInvalid_ShouldThrowException()
        {
            // Arrange -> prapare the objects
            var repo = new BugsRepository(new BugTrackerDbContext());
            var invalidBug = new Bug() { Text = null };

            // Act -> perform some logic
            repo.Add(invalidBug);
            repo.SaveChanges();

            // Assert -> expect an exception
        }

        // TODO: test also Update, Delete, Find-by-Criteria, etc.
    }
}
