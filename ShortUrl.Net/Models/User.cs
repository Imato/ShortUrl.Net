using LiteDB;

namespace ShortUrlNet.Models
{
    public class User : CollectionItem
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public ObjectId SessionId { get; set; }
    }
}
