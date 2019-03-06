using FluentAssertions;
using Practice.DynamoDb;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Practice.Tests.DynamoDb
{
    public class BlogContextTests : IDisposable
    {
        private BlogContextFactory _ContextFactory { get; }

        public BlogContextTests()
        {
            _ContextFactory = new BlogContextFactory(new Uri("http://localhost:8000"));
        }

        [Fact(Skip = "Run LocalDB")]
        public async Task WhenBlogIsSaved_ThenSameBlogIsRetrieved()
        {
            var body   = "Really smart sounding blog post because I'm a hipster.";
            var author = "ME!";

            var bodyStream = new MemoryStream();

            using (var writer = new StreamWriter(bodyStream, Encoding.UTF8, 1024, true))
            {
                writer.Write(body);
            }

            bodyStream.Position = 0L;

            var blog = new Blog(DateTime.UtcNow, author, bodyStream);

            var context = await _ContextFactory.BuildContext();

            var actualBlog = await context.Save(blog);

            var actualBlogBody = string.Empty;

            using (var reader = new StreamReader(actualBlog.Body, Encoding.UTF8))
            {
                actualBlogBody = reader.ReadToEnd();
            }

            actualBlogBody.Should().Be(body);
        }

        public void Dispose()
        {
            var context = _ContextFactory.BuildContext().Result;
            context.DropTable().Wait();
            _ContextFactory.Dispose();
        }
    }
}
