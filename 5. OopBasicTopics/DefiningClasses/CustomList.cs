namespace DefiningClasses
{
    using System;

    public class CustomList<T>
    {
        private T[] elements;
        private int count;

        public CustomList(int size = 16)
        {
            this.elements = new T[size];
            this.count = 0;
        }

        public void Add(T element)
        {
            this.elements[this.count] = element;
            this.count++;
        }

        public void Clear()
        {
            for (int i = 0; i < this.count; i++)
            {
                this.elements[i] = default(T);
            }
        }

        public void Print()
        {
            for (int i = 0; i < this.count; i++)
            {
                Console.WriteLine(this.elements[i]);
            }
        }
    }
}
