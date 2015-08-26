namespace RpgGame.Tests
{
    using System;
    using DependencyInversion.RpgGame;
    using Moq;

    public class RandomNumberProviderMock
    {
        public RandomNumberProviderMock()
        {
            this.Setup();
        }

        public IRandomNumberProvider RandomNumberProvider { get; set; }

        private void Setup()
        {
            var mock = new Mock<IRandomNumberProvider>();

            mock.Setup(rnp => rnp.GetRandomNumber(
                    It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0);

            mock.Setup(rnp => rnp.GetRandomNumber(
                    It.IsInRange(10, 20, Range.Inclusive), 
                    It.IsInRange(10, 20, Range.Inclusive)))
                .Throws(new InvalidOperationException());

            this.RandomNumberProvider = mock.Object;
        }
    }
}
