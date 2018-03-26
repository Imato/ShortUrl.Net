using LiteDB;
using System;

namespace ShortUrlNet.Models
{
    public class ApiKey : CollectionItem
    {     
        public ObjectId UserKey { get; set; }        
        public DateTime CreateDate { get; set; }
        public DateTime ExpireDate { get; set; }        
    }
}
