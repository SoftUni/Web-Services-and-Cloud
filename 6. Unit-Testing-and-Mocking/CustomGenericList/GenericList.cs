using System;

namespace CustomGenericList
{
    // TODO: Finish testing, still bugs to fix
    public class GenericList<T> where T : IComparable<T>
    {
        private T[] arr;
        private int count;

        public GenericList()
        {
            this.arr = new T[16];
            this.Count = 0;
        }

        public GenericList(int capacity)
        {
            this.arr = new T[capacity];
            this.Count = 0;
        }

        public int Count
        {
            get { return this.count; }
            set { this.count = value; }
        }

        public int Capacity
        {
            get { return this.arr.Length; }
        }

        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                return this.arr[index];
            }
        }

        public void Add(T value)
        {
            if (Count == Capacity)
            {
                Array.Resize(ref arr, arr.Length * 2);
            }

            arr[Count] = value;
            Count++;
        }

        public void InsertAt(T value, int index)
        {
            CheckIndex(index);
            T[] buffer = new T[Capacity + 1];

            for (int i = 0, position = 0; i < arr.Length - 1; position++)
            {
                if (position == index)
                {
                    buffer[position] = value;
                    continue;
                }
                else
                {
                    buffer[position] = arr[i];
                    i++;
                }
            }

            arr = buffer;
            Count++;
        }

        public void Remove(T value)
        {
            RemoveAt(Find(value));
        }

        public void RemoveAt(int index)
        {
            CheckIndex(index);
            T[] buffer = new T[Capacity];

            for (int i = 0, position = 0; i < arr.Length; i++, position++)
            {
                if (i == index)
                {
                    position--;
                    continue;
                }
                else
                {
                    buffer[position] = arr[i];
                }
            }

            arr = buffer;
            Count--;
        }

        public void Clear()
        {
            Array.Clear(arr, 0, arr.Length);
            Count = 0;
        }

        public int Find(T element)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.arr[i].Equals(element))
                {
                    return i;
                }
            }

            return -1;
        }

        public void CheckIndex(int index)
        {
            if ((index < 0) || (index >= Count))
            {
                throw new IndexOutOfRangeException("Index must be in range [0, count]");
            }
        }

        public T Min()
        {
            T min = arr[0];
            for (int i = 1; i < Count; i++)
            {
                T listItem = arr[i];
                if (listItem.CompareTo(min) < 0)
                {
                    min = arr[i];
                }
            }

            return min;
        }

        public T Max()
        {
            T max = arr[0];
            for (int i = 1; i < Count; i++)
            {
                T listItem = arr[i];
                if (listItem.CompareTo(max) > 0)
                {
                    max = arr[i];
                }
            }

            return max;
        }

        public override string ToString()
        {
            string str = "";
            for (int index = 0; index < Count; index++)
            {
                str += string.Format("[{0}] = {1}\n", index, arr[index]);
            }
            return str;
        }
    }
}
