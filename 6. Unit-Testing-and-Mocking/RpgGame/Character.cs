namespace RpgGame
{
    using System;
    using System.Collections.Generic;

    public class Character
    {
        private static Random random;
        private readonly List<Item> possibleItemDrops;

        public Character(List<Item> items)
        {
            random = new Random();
            this.possibleItemDrops = items;
        }

        public Item DropItem()
        {
            var randomIndex = random.Next(0, this.possibleItemDrops.Count);

            return this.possibleItemDrops[randomIndex];
        }
    }

    public interface IRandomNumberProvider
    {
        int GetRandomNumber(int min, int max);
    }

    public class RandomNumberProvider : IRandomNumberProvider
    {
        private Random random;

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
