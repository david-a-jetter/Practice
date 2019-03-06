using System;
using System.IO;

namespace Practice.DynamoDb
{
    public class Blog
    {
        public Guid? Id          { get; }

        public int? Version      { get; }

        public DateTime Created  { get; }

        public DateTime Updated  { get; }

        public string Author     { get; }

        public MemoryStream Body { get; }

        internal Blog(
            Guid? id,
            int? version,
            DateTime created,
            DateTime updated,
            string author,
            MemoryStream body)
        {
            Id      = id;
            Version = version;
            Created = created;
            Updated = updated;
            Author  = author;
            Body    = body;
        }

        public Blog(DateTime created, string author, MemoryStream body)
        {
            Created = created;
            Updated = created;
            Author  = author;
            Body    = body;
        }

        public Blog Update(DateTime updated, string author, MemoryStream body)
        {
            var blog = new Blog(
                this.Id,
                this.Version,
                this.Created,
                updated,
                author,
                body);

            return blog;
        }
    }
}
