using FluentAssertions;
using Practice.Copy;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Practice.Tests.Copy
{
    public class CopyTests
    {
        [Fact]
        public async Task WhenDirectoryIsCopied_ThenAllFilesExistInNewTarget()
        {
            // Arrange
            var baseDirectory = @"C:\base";
            var targetDirectory = @"C:\target";
            var expectedDirectory = Path.Combine(targetDirectory, "base");
            var files = new Dictionary<string, MockFileData>
            {
                { Path.Combine(baseDirectory, "myfile.txt"), new MockFileData("Testing is meh.") },
                { Path.Combine(baseDirectory, "jQuery.js"), new MockFileData("some js") },
                { Path.Combine(baseDirectory, "image.gif"), new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) }
            };
            var fileSystem = new MockFileSystem(files);
            var mapper = new DirectoryCopier(fileSystem);
            await mapper.CopyDirectory(baseDirectory, targetDirectory);

            foreach(var file in files)
            {
                var fileName = Path.GetFileName(file.Key);
                var newPath = Path.Combine(expectedDirectory, fileName);
                var copied = fileSystem.GetFile(newPath);

                copied.Contents.Should().BeEquivalentTo(file.Value.Contents);
            }
        }
    }
}
