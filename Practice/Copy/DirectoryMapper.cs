using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace Practice.Copy
{
    internal class DirectoryMapper
    {
        private IFileSystem _FileSystem { get; }
        public DirectoryMapper(IFileSystem fileSystem)
        {
            _FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public ISet<string> GetAllFilePaths(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory)) throw new ArgumentNullException(nameof(directory));
            if (!_FileSystem.Directory.Exists(directory))
            {
                throw new ArgumentException($"Directory does not exist: {directory}");
            }

            var filePaths = new HashSet<string>();
            var subDirectories = new Queue<string>();
            subDirectories.Enqueue(directory);

            while (subDirectories.Count > 0)
            {
                var nextDirectory = subDirectories.Dequeue();
                AppendFilesAndSubdirectories(nextDirectory, filePaths, subDirectories);
            }

            return filePaths;
        }

        private void AppendFilesAndSubdirectories(
            string directory,
            ISet<string> filePaths,
            Queue<string> directoryQueue)
        {
            var files = _FileSystem.Directory.GetFiles(directory);

            foreach (var file in files)
            {
                filePaths.Add(file);
            }

            var subDirectories = _FileSystem.Directory.GetDirectories(directory);

            foreach (var subDirectory in subDirectories)
            {
                directoryQueue.Enqueue(subDirectory);
            }
        }
    }
}
