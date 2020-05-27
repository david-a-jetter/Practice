using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Copy
{
    internal class FileCopier
    {
        private IFileSystem _FileSystem { get; }

        public FileCopier(IFileSystem fileSystem)
        {
            _FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public async Task CopyFiles(ISet<string> sourcePaths, string targetDirectory)
        {
            var copyTasks = sourcePaths.Select(path => CopyFile(path, targetDirectory));

            await Task.WhenAll(copyTasks);
        }

        private async Task CopyFile(string sourcePath, string targetDirectory)
        {
            var targetPath = BuildTargetPath(sourcePath, targetDirectory);
            var targetDir = _FileSystem.Path.GetDirectoryName(targetPath);

            if (! _FileSystem.Directory.Exists(targetDir))
            {
                _FileSystem.Directory.CreateDirectory(targetDir);
            }

            _FileSystem.File.Copy(sourcePath, targetPath);
        }

        private string BuildTargetPath(string sourcePath, string targetDirectory)
        {
            var separator = _FileSystem.Path.DirectorySeparatorChar;
            var pathComponents = sourcePath.Split(separator);
            pathComponents[0] = targetDirectory;
            var targetPath = String.Join(separator.ToString(), pathComponents);

            return targetPath;
        }

        private Stream CreateFileStream(string path)
        {
            var stream = _FileSystem.FileStream.Create(
                path: path,
                mode: FileMode.Open,
                access: FileAccess.Read,
                share: FileShare.Read,
                bufferSize: 4096,
                useAsync: true);

            return stream;
        }
    }
}
