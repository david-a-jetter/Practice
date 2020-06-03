using FluentAssertions;
using Practice.Copy;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Threading.Tasks;
using Xunit;

namespace Practice.Tests.Copy
{
    public class CopyTests
    {
        [Fact]
        public async Task WhenDirectoryIsCopied_ThenAllFilesContentsExistInNewTarget()
        {
            // Arrange
            var baseDirectory = @"C:\base";
            var targetDirectory = @"C:\target";
            var files = new Dictionary<string, MockFileData>
            {
                { Path.Combine(baseDirectory, "myfile.txt"), new MockFileData("Testing is meh.") },
                { Path.Combine(baseDirectory, @"subDir\myfile.txt"), new MockFileData("Testing is GREAT.") },
                { Path.Combine(baseDirectory, @"subDir\doubleSub\two.txt"), new MockFileData("Testing is MEH!!!!.") },
                { Path.Combine(baseDirectory, @"subDir\doubleSub\deep\two.txt"), new MockFileData("Testing is MEH!!!!.") },
                { Path.Combine(baseDirectory, @"subDir\otherSub\four.txt"), new MockFileData("Testing is potato.") },
                { Path.Combine(baseDirectory, @"subDir\otherSub\deeper\five.txt"), new MockFileData("Testing is nested potatoes.") },
                { Path.Combine(baseDirectory, "jQuery.js"), new MockFileData("some js") },
                { Path.Combine(baseDirectory, "image.gif"), new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) }
            };
            var fileSystem = new MockFileSystem(files);
            var copier = new DirectoryCopier(fileSystem);

            //Act
            await copier.CopyDirectory(baseDirectory, targetDirectory);

            //Assert
            foreach(var file in files)
            {
                var separator = fileSystem.Path.DirectorySeparatorChar;
                var pathComponents = file.Key.Split(separator);
                pathComponents[0] = targetDirectory;
                var expectedDirectory = String.Join(separator.ToString(), pathComponents);
                var copied = fileSystem.GetFile(expectedDirectory);

                copied.Contents.Should().BeEquivalentTo(file.Value.Contents);
            }
        }
    }
}
