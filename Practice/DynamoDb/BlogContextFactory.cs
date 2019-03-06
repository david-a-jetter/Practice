using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Practice.DynamoDb
{
    public class BlogContextFactory : IDisposable
    {
        private Lazy<Task<BlogContext>>  _LazyContext { get; }

        public BlogContextFactory(Uri serviceUrl)
        {
            _LazyContext = new Lazy<Task<BlogContext>>(
                async ()  => await BuildBlogContext(serviceUrl));
        }

        public async Task<IBlogContext> BuildContext()
        {
            return await _LazyContext.Value;
        }

        private async Task<BlogContext> BuildBlogContext(Uri serviceUrl)
        {
            var client = new AmazonDynamoDBClient(
                new AmazonDynamoDBConfig
                {
                    ServiceURL = serviceUrl.ToString()
                });

            await CreateTableIfNeeded(client);

            var context = new BlogContext(client);

            return context;
        }

        private static async Task CreateTableIfNeeded(IAmazonDynamoDB client)
        {
            var tables = await client.ListTablesAsync();

            if (! tables.TableNames.Contains(BlogConstants.TableName))
            {
                var tableRequest = new CreateTableRequest(
                    BlogConstants.TableName,
                    new List<KeySchemaElement>
                    {
                    new KeySchemaElement(BlogConstants.IdColumn, KeyType.HASH)
                    },
                    new List<AttributeDefinition>
                    {
                    new AttributeDefinition(BlogConstants.IdColumn, ScalarAttributeType.S)
                    },
                    new ProvisionedThroughput(10, 10));

                await client.CreateTableAsync(tableRequest);
            }
        }

        public void Dispose()
        {
            if (_LazyContext.IsValueCreated)
            {
                _LazyContext.Value.Dispose();
            }
        }
    }
}
