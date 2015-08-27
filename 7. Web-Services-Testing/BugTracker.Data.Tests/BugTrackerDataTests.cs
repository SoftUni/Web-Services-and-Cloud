namespace BugTracker.Data.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BugTracker.Data;
    using System.Data.Entity.Validation;

    [TestClass]
    public class BugTrackerDataTests
    {
        [TestMethod]
        public void AddBug_WhenBugIsValid_ShouldAddToDb()
        {
            // Arrange -> prepare the objects
            var bug = new Bug()
            {
                Text = "Sample New Bug",
                DateCreated = DateTime.Now,
                Status = BugStatus.New
            };

            var dbContext = new BugTrackerDbContext();

            // Act -> perform some logic
            dbContext.Bugs.Add(bug);
            dbContext.SaveChanges();

            // Assert -> validate the results
            var bugInDb = dbContext.Bugs.Find(bug.Id);

            Assert.IsNotNull(bugInDb);
            Assert.AreEqual(bug.Text, bugInDb.Text);
            Assert.AreEqual(bug.Status, bugInDb.Status);
            Assert.AreEqual(bug.DateCreated, bugInDb.DateCreated);
            Assert.IsTrue(bugInDb.Id != 0);
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddBug_WhenBugIsInvalid_ShouldThrowException()
        {
            // Arrange -> prapare the objects
            var dbContext = new BugTrackerDbContext();
            var invalidBug = new Bug() { Text = null };

            // Act -> perform some logic
            dbContext.Bugs.Add(invalidBug);
            dbContext.SaveChanges();

            // Assert -> expect an exception
        }

        // TODO: test also Update, Delete, Find-by-Criteria, etc.
    }
}
