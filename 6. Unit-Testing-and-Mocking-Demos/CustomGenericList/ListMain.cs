namespace CustomGenericList
{
    using System;

    public class ListMain
    {
        static void Main()
        {
            var list = new GenericList<int>();
            list.Add(5);
            list.Add(4);
            list.Add(2);
            list.Add(4);
            
            list.Remove(4);
            list.RemoveAt(0);

            Console.WriteLine(list);
        }
    }
}
