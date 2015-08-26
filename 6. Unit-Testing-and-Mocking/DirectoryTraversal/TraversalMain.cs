namespace DirectoryTraversal
{
    using System;

    public class TraversalMain
    {
        static void Main()
        {
            var traverser = new DirectoryTraverser(@"C:\");

            var children = traverser.GetChildDirectories();
            foreach (var child in children)
            {
                Console.WriteLine(child);
            }

            Console.WriteLine(traverser.CurrentDirectory);
        }
    }
}
