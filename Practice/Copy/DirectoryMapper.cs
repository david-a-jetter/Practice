using System;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace Practice.Copy
{
    internal class DirectoryMapper
    {
        private IFileSystem _FileSystem { get; }
        private string _Directory { get; }
        private ISet<string> _Paths { get; }

        public DirectoryMapper(IFileSystem fileSystem, string baseDirectory)
        {
            _FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            if (string.IsNullOrWhiteSpace(baseDirectory)) throw new ArgumentNullException(nameof(baseDirectory));
            if (!_FileSystem.Directory.Exists(baseDirectory))
            {
                throw new ArgumentException($"Directory does not exist: {baseDirectory}");
            }

            _Directory = baseDirectory;
            _Paths = new HashSet<string>();
        }

        public ISet<string> GetAllFilePaths()
        {
            AddFilesForDirectory(_Directory);

            return _Paths;
        }

        private void AddFilesForDirectory(string directory)
        {
            var files = _FileSystem.Directory.GetFiles(directory);
            foreach (var file in files)
            {
                _Paths.Add(file);
            }
        }
    }
}
