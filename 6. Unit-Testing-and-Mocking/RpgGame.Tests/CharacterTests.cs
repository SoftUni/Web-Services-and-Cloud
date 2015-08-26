namespace RpgGame.Tests
{
    using System.Collections.Generic;
    using DependencyInversion.RpgGame;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CharacterTests
    {
        private IRandomNumberProvider randomNumberProvider;

        [TestInitialize]
        public void TestInit()
        {
            var mock = new RandomNumberProviderMock();
            this.randomNumberProvider = mock.RandomNumberProvider;
        }

        [TestMethod]
        public void Character_Should_Drop_First_Item()
        {
            // Arrange
            var possibleItemDrops = new List<Item>
            {
                new Item("Axe", 25, 5),
                new Item("Shield", 5, 50),
                new Item("Resilience Potion", 0, 30)
            };

            // Act
            var character = new Character(
                possibleItemDrops,
                this.randomNumberProvider);


            // Assert
            for (int i = 0; i < 1000; i++)
            {
                var item = character.DropItem();
                Assert.AreEqual("Axe", item.Name);
            }
        }
    }
}
