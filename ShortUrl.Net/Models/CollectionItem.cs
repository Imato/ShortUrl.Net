﻿

using LiteDB;

namespace ShortUrlNet.Models
{
    public class CollectionItem
    {
        [BsonId]
        public ObjectId Key { get; set; }

        public override string ToString()
        {
            return Key.ToString();
        }
    }
}
