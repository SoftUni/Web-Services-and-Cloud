namespace DependencyInversion.RpgGame
{
    using System.Collections.Generic;

    public class Character
    {
        private readonly List<Item> possibleItemDrops;

        public Character(
            List<Item> items, 
            IRandomNumberProvider randomNumberProvider)
        {
            this.possibleItemDrops = items;
            this.RandomNumberProvider = randomNumberProvider;
        }

        public IRandomNumberProvider RandomNumberProvider { get; set; }

        public Item DropItem()
        {
            var randomIndex = this.RandomNumberProvider
                .GetRandomNumber(0, this.possibleItemDrops.Count);

            return this.possibleItemDrops[randomIndex];
        }
    }
}
