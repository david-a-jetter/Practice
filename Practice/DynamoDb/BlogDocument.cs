using Amazon.DynamoDBv2.DataModel;
using System;
using System.IO;

namespace Practice.DynamoDb
{
    [DynamoDBTable(BlogConstants.TableName)]
    internal class BlogDocument
    {
        [DynamoDBHashKey]
        internal string Id         { get; set; }

        [DynamoDBVersion]
        internal int? Version      { get; set; }

        internal DateTime Created  { get; set; }

        internal DateTime Updated  { get; set; }

        internal string Author     { get; set; }

        internal MemoryStream Body { get; set; }
    }
}
