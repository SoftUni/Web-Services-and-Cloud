namespace DependencyInversion.DirectoryTraversal
{
    using System.Collections.Generic;
    using System.IO;

    public class DirectoryProvider : IDirectoryProvider
    {
        public IEnumerable<string> GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }
    }
}
