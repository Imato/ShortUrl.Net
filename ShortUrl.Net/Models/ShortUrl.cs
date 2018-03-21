using LiteDB;
using System;

namespace ShortUrlNet.Models
{
    public class ShortUrl : CollectionItem
    {
        public string Url { get; set; }
        public DateTime CreateDate { get; set; }
        public int ViewCount { get; set; }
        public ObjectId UserKey { get; set; }
    }
}
