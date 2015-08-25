namespace DirectoryTraversal
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class DirectoryTraverser
    {
        public DirectoryTraverser(string directory)
        {
            this.CurrentDirectory = directory;
        }

        public string CurrentDirectory { get; set; }

        public IEnumerable<string> GetChildDirectories()
        {
            var directories = Directory.GetDirectories(this.CurrentDirectory);

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
