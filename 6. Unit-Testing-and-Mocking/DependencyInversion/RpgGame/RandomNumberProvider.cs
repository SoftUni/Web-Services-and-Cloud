namespace DependencyInversion.RpgGame
{
    using System;

    public class RandomNumberProvider : IRandomNumberProvider
    {
        private static Random random;

        public RandomNumberProvider()
        {
            random = new Random();
        }

        public int GetRandomNumber(int min, int max)
        {
            return random.Next(min, max + 1);
        }
    }
}
