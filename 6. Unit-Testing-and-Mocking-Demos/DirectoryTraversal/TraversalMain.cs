namespace DirectoryTraversal
{
    using System;

    public class TraversalMain
    {
        static void Main()
        {
            var traverser = new DirectoryTraverser(
                @"C:\SVN\web-services-and-cloud\August-2015");

            var children = traverser.GetChildDirectories();
            foreach (var child in children)
            {
                Console.WriteLine(child);
            }

            Console.WriteLine(traverser.CurrentDirectory);
        }
    }
}
