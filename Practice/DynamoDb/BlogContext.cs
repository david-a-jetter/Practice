using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Practice.DynamoDb
{
    internal class BlogContext : IBlogContext, IDisposable
    {
        internal IAmazonDynamoDB _Client { get; }

        internal BlogContext(IAmazonDynamoDB client)
        {
            _Client  = client  ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<Blog> Save(Blog inputBlog)
        {
            Blog loadedBlog;

            using (var context = new DynamoDBContext(_Client))
            {
                if (inputBlog is null) throw new ArgumentNullException(nameof(inputBlog));

                var compressedStream = new MemoryStream();

                using (var zip = new GZipStream(compressedStream, CompressionMode.Compress, true))
                {
                    await inputBlog.Body.CopyToAsync(zip);
                }

                var blogId = inputBlog.Id ?? Guid.NewGuid();

                var blogDoc = new BlogDocument
                {
                    Id      = blogId.ToString(),
                    Version = inputBlog.Version,
                    Created = inputBlog.Created,
                    Updated = inputBlog.Updated,
                    Author  = inputBlog.Author,
                    Body    = compressedStream
                };

                await context.SaveAsync<BlogDocument>(blogDoc);

                loadedBlog = await LoadBlog(context, blogId);
            }

            return loadedBlog;
        }

        public async Task<Blog> GetBlog(Guid blogId)
        {
            Blog outputBlog;

            using (var context = new DynamoDBContext(_Client))
            {
                outputBlog = await LoadBlog(context, blogId);
            }

            return outputBlog;
        }

        private async Task<Blog> LoadBlog(IDynamoDBContext context, Guid BlogId)
        {
            var blogDoc = await context.LoadAsync<BlogDocument>(BlogId.ToString());
            var blog    = await BuildBlog(blogDoc);

            return blog;
        }

        private async Task<Blog> BuildBlog(BlogDocument blogDocument)
        {
            if (blogDocument is null) throw new ArgumentNullException(nameof(blogDocument));

            var uncompressedBody = new MemoryStream();

            using (var zip = new GZipStream(blogDocument.Body, CompressionMode.Decompress, true))
            {
                await zip.CopyToAsync(uncompressedBody);
            }

            uncompressedBody.Position = 0L;

            var blog = new Blog(
                new Guid(blogDocument.Id),
                blogDocument.Version,
                blogDocument.Created,
                blogDocument.Updated,
                blogDocument.Author,
                uncompressedBody);

            return blog;
        }

        public async Task DropTable()
        {
            await _Client.DeleteTableAsync(BlogConstants.TableName);
        }

        public void Dispose()
        {
            _Client.Dispose();
        }
    }
}
