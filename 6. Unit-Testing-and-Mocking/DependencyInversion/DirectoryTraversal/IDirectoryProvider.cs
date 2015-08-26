namespace DependencyInversion.DirectoryTraversal
{
    using System.Collections.Generic;

    public interface IDirectoryProvider
    {
        IEnumerable<string> GetDirectories(string path);
    }
}
