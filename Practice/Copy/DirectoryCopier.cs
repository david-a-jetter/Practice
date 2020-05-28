using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Copy
{
    public class DirectoryCopier
    {
        private IFileSystem _FileSystem { get; }
        public DirectoryCopier(IFileSystem fileSystem)
        {
            _FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public async Task CopyDirectory(string source, string target)
        {
            var mapper = new DirectoryMapper(_FileSystem);
            var filesToCopy = mapper.GetAllFilePaths(source);

            var copier = new FileCopier(_FileSystem);
            await copier.CopyFiles(filesToCopy, target);
        }
    }
}
