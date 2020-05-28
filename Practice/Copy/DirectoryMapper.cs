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

            var filePaths = GetFilePathsInDirectory(directory);

            return filePaths;
        }

        private HashSet<string> GetFilePathsInDirectory(string directory)
        {
            var files = _FileSystem.Directory.GetFiles(directory);
            var paths = new HashSet<string>();
            foreach (var file in files)
            {
                paths.Add(file);
            }

            var subDirectories = _FileSystem.Directory.GetDirectories(directory);

            foreach (var subDirectory in subDirectories)
            {
                var subPaths = GetFilePathsInDirectory(subDirectory);

                foreach (var path in subPaths)
                {
                    paths.Add(path);
                }
            }

            return paths;
        }
    }
}
