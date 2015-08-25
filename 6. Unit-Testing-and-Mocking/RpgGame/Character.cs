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
            this.possibleItemDrops = items;
            Character.random = new Random();
        }

        public Item DropItem()
        {
            var randomIndex = random.Next(0, this.possibleItemDrops.Count);

            return this.possibleItemDrops[randomIndex];
        }
    }
}
