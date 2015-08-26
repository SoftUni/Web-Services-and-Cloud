namespace DependencyInversion.DirectoryTraversal
{
    using System.Collections.Generic;
    using System.Linq;

    class DirectoryTraverser
    {
        public DirectoryTraverser(string directory, IDirectoryProvider directoryProvider)
        {
            this.CurrentDirectory = directory;
            this.DirectoryProvider = directoryProvider;
        }

        public string CurrentDirectory { get; set; }

        public IDirectoryProvider DirectoryProvider { get; set; }

        public IEnumerable<string> GetChildDirectories()
        {
            var directories = this.DirectoryProvider.GetDirectories(this.CurrentDirectory);

            var directoryNames = new List<string>(directories.Count());
            foreach (var directory in directories)
            {
                int lastBackSlash = directory.LastIndexOf("\\");
                string directoryName = directory.Substring(lastBackSlash);

                directoryNames.Add(directoryName);
            }

            directoryNames.Sort();

            return directoryNames;
        }
    }
}
