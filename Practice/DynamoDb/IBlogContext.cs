using System;
using System.Threading.Tasks;

namespace Practice.DynamoDb
{
    public interface IBlogContext
    {
        Task<Blog> Save(Blog inputBlog);

        Task<Blog> GetBlog(Guid blogId);

        Task DropTable();
    }
}
